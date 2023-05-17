using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float nextWaypointDistance;
    public MovingDirection movingDirection;

    #region LOS Detection
    public float LOS_DetectionRange;
    public int LOS_DetectionAngle;
    public Transform viewPoint;
    private LayerMask targetLayer;
    public LayerMask hitableLayers;
    private bool isTargetInLOS;
    public event EventHandler<TargetDetectedEventArgs> TargetDetected;
    #endregion

    #region Memory Following
    private bool isTargetPositionMemorized;
    #endregion

    private Path path = null;
    private int currentWaypoint = 0;
    private bool followingPath;
    private bool allowPathPloting;

    private Seeker seeker;
    private Rigidbody2D rb;

    //Player Detection Timer
    private DateTime lastSeenTargetTime;
    public float targetMemoryMS;

    private bool isTargetFollowable;

    private void Start()
    {
        allowPathPloting = true;
        lastSeenTargetTime = DateTime.Now;
        movingDirection = MovingDirection.N;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        followingPath = false;
        targetLayer = LayerMask.NameToLayer("Player");
        InvokeRepeating("CheckTargetDetection", 0, 0.1f);
    }
    private void BuildPathToTarget()
    {
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Debug.Log(p.vectorPath.Count);
            path = p;
            currentWaypoint = 0;
            followingPath = true;
        }
    }

    private void Update()
    {
        CheckTargetMemoryDetection();
        CheckTargetDetection();
        isTargetFollowable = isTargetInLOS || isTargetPositionMemorized;
        if (allowPathPloting && isTargetFollowable) {
            BuildPathToTarget();
        }
    }

    private void FixedUpdate()
    {
        //Idea implement different behaviors based on the enemy current state (following, returning to position, wandering)
        if (followingPath)
        {
            FollowPathToTarget();
        }
    }

    Vector2 directionToPlayer = new Vector2();
    float distanceToPlayer = 0;
    float angleToPlayer = 0;

    public bool IsTargetDetected { get => isTargetInLOS;}

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
        directionToPlayer = (Vector2)(target.position - viewPoint.position);
        distanceToPlayer = directionToPlayer.magnitude;
        directionToPlayer.Normalize();
        angleToPlayer = Vector2.Angle(viewPoint.up, directionToPlayer);

        if (distanceToPlayer <= LOS_DetectionRange && angleToPlayer <= LOS_DetectionAngle)
        {
            RaycastHit2D hit = Physics2D.Raycast(viewPoint.position, directionToPlayer, distanceToPlayer, hitableLayers);
            Debug.DrawRay((Vector3)viewPoint.position, directionToPlayer * distanceToPlayer);
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

        if (currentWaypoint >= path.vectorPath.Count)
        {
            StopFollowingPath();
            return;
        }
    }

    private void StopFollowingPath()
    {
        rb.velocity = Vector2.zero;
        RefreshCurrentPath();
    }

    private void RefreshCurrentPath()
    {
        path = null;
        currentWaypoint = 0;
        followingPath = false;
        allowPathPloting = true;
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
