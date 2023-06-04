using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float nextWaypointDistance;
    public MovingDirection movingDirection;
    public AILogicState currentState = AILogicState.idle;
    private AILogicState idleState;

    #region LOS Detection
    public float LOS_DetectionRange;
    public int LOS_DetectionAngle;
    public Transform viewPoint;
    private LayerMask targetLayer;
    public LayerMask hitableLayers;
    private bool isTargetInLOS;
    public event EventHandler<TargetDetectedEventArgs> TargetDetected;
    public float reachedPlayerDistance;
    #endregion

    #region Memory Following
    private bool isTargetPositionMemorized;
    #endregion

    public float guaranteedDetectionRadius;

    private Path path = null;
    private int currentWaypoint = 0;
    private bool followingPathToTarget;
    private bool allowPathPloting;

    private Seeker seeker;
    private Rigidbody2D rb;

    //Player Detection Timer
    private DateTime lastSeenTargetTime;
    public float targetMemoryMS;

    private bool isTargetFollowable;

    private VitalityManager targetVitalityManager;

    #region PATROL
    [SerializeField]private PatrolPath patrolPath;
    public bool isPatroller = false;
    private DateTime stoppedFollowingTargetTime;
    private Transform targetCheckPoint;
    private bool patrolling = false;
    #endregion


    Vector2 directionToPlayer = new Vector2();
    float distanceToPlayer = 0;
    float angleToPlayer = 0;

    public bool IsTargetDetected { get => isTargetInLOS; }

    private void Start()
    {
        currentState = AILogicState.idle;
        idleState = AILogicState.idle;
        allowPathPloting = true;
        lastSeenTargetTime = DateTime.Now;
        movingDirection = MovingDirection.N;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        followingPathToTarget = false;
        targetLayer = LayerMask.NameToLayer("Player");
        InvokeRepeating("CheckTargetDetection", 0, 0.1f);
        targetVitalityManager = target.gameObject.GetComponent<VitalityManager>();

        if (isPatroller)
        {
            idleState = AILogicState.followingPath;
            currentState = AILogicState.followingPath;
            targetCheckPoint = patrolPath.GetStartingPoint();
        }
    }
    private void BuildPathToTarget()
    {
        if (currentState == AILogicState.followingPlayer && allowPathPloting)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        else if (currentState == AILogicState.followingPath)
            seeker.StartPath(rb.position, targetCheckPoint.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            followingPathToTarget = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlayerAlive)
        {
            CheckTargetMemoryDetection();
            CheckTargetDetection();
            isTargetFollowable = isTargetInLOS || isTargetPositionMemorized;
            if (isTargetFollowable)
            {
                currentState = AILogicState.followingPlayer;
                ResetPatrolling();
            }
            else if(isPatroller)
            {
                currentState = AILogicState.followingPath;
            }
            BuildPathToTarget();
        }
    }

    private void FixedUpdate()
    {
        //Idea implement different behaviors based on the enemy current state (following, returning to position, wandering)
        if (followingPathToTarget)
        {
            FollowPathToTarget();
        }
    }

    /// <summary>
    /// If the time passed since last detection of the target is greater than the targetMemoryMS then the enemy forgets about the target
    /// </summary>
    private void CheckTargetMemoryDetection()
    {
        DateTime now = DateTime.Now;
        if ((now - lastSeenTargetTime).TotalMilliseconds >= targetMemoryMS)
        {
            isTargetPositionMemorized = false;
        }
    }

    private bool CheckTargetDetection()
    {
        if (!GameManager.Instance.IsPlayerAlive)
            return false;
        directionToPlayer = (Vector2)(target.position - viewPoint.position);
        distanceToPlayer = directionToPlayer.magnitude;
        directionToPlayer.Normalize();
        angleToPlayer = Vector2.Angle(viewPoint.up, directionToPlayer);

        //Assured detection distance
        if (distanceToPlayer <= guaranteedDetectionRadius)
        {
            RaycastHit2D hit = Physics2D.Raycast(viewPoint.position, directionToPlayer, distanceToPlayer, hitableLayers);
            if (hit.collider != null && hit.transform.gameObject.layer == targetLayer.value)
            {
                isTargetInLOS = true;
                isTargetPositionMemorized = true;
                lastSeenTargetTime = DateTime.Now;
                OnTargetDetected(new TargetDetectedEventArgs(target));
                return true;
            }
        }

        if (distanceToPlayer <= LOS_DetectionRange && angleToPlayer <= LOS_DetectionAngle)
        {
            RaycastHit2D hit = Physics2D.Raycast(viewPoint.position, directionToPlayer, distanceToPlayer, hitableLayers);
            if(hit.collider != null && hit.transform.gameObject.layer == targetLayer.value)
            {
                isTargetInLOS = true;
                isTargetPositionMemorized = true;
                lastSeenTargetTime = DateTime.Now;
                OnTargetDetected(new TargetDetectedEventArgs(target));
                return true;
            }
        }

        isTargetInLOS = false;
        return false;
    }

    private void FollowPathToTarget()
    {
        if(allowPathPloting)
            allowPathPloting = false;
        Vector2 movementVector = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        movingDirection = MovingDirectionHelper.detectMovingDirection(movementVector);
        Vector2 lookDirection = (Vector2)target.position - rb.position;
        Vector2 velocity = movementVector * speed;
        rb.velocity = velocity;
        rb.rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(distanceToPlayer <= reachedPlayerDistance)
        {
            StopFollowingPath();
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            StopFollowingPath();
            return;
        }
    }

    private void StopFollowingPath()
    {
        if(currentState == AILogicState.followingPlayer)
        {
            rb.velocity = Vector2.zero;
            currentState = idleState;
            stoppedFollowingTargetTime = DateTime.Now;
            RefreshCurrentPath();
        }
        else if(currentState == AILogicState.followingPath)
        {
            targetCheckPoint = patrolPath.GetNextCheckPoint();
            BuildPathToTarget();
        }
    }

    private void RefreshCurrentPath()
    {
        path = null;
        currentWaypoint = 0;
        if(currentState == AILogicState.followingPlayer)
            followingPathToTarget = false;
        allowPathPloting = true;
    }

    private void ResetPatrolling()
    {
        if (isPatroller)
        {
            targetCheckPoint = patrolPath.GetStartingPoint();
            patrolling = false;
        }
    }

    protected virtual void OnTargetDetected(TargetDetectedEventArgs e)
    {
        TargetDetected?.Invoke(this, e);
    }
}

public class TargetDetectedEventArgs : EventArgs
{
    public Transform target;

    public TargetDetectedEventArgs(Transform position)
    {
        target = position;
    }
}
