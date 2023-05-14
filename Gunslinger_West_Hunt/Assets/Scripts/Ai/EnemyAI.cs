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
    private bool isTargetDetected;
    public event EventHandler<TargetDetectedEventArgs> TargetDetected;
    #endregion

    private Path path = null;
    private int currentWaypoint = 0;
    private bool followingPath;

    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
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
            path = p;
            currentWaypoint = 0;
            followingPath = true;
        }
    }

    private void Update()
    {
        
        if (isTargetDetected && !followingPath)
        {
            BuildPathToTarget();
        }
    }

    private void FixedUpdate()
    {
        if (followingPath && path != null) 
        {
            FollowPathToTarget();
        }
    }

    Vector2 directionToPlayer = new Vector2();
    float distanceToPlayer = 0;
    float angleToPlayer = 0;

    public bool IsTargetDetected { get => isTargetDetected;}

    private void CheckTargetDetection()
    {
        directionToPlayer = (Vector2)(target.position - viewPoint.position);
        distanceToPlayer = directionToPlayer.magnitude;
        directionToPlayer.Normalize();
        angleToPlayer = Vector2.Angle(viewPoint.up, directionToPlayer);

        if (distanceToPlayer <= LOS_DetectionRange && angleToPlayer <= LOS_DetectionAngle)
        {
            RaycastHit2D hit = Physics2D.Raycast(viewPoint.position, directionToPlayer, distanceToPlayer, hitableLayers);
            if(hit.collider != null && hit.transform.gameObject.layer == targetLayer.value)
            {
                isTargetDetected = true;
                OnTargetDetected(new TargetDetectedEventArgs(target));
                return;
            }
        }
        isTargetDetected = false;
    }

    private void RefreshCurrentPath()
    {
        path = null;
        currentWaypoint = 0;
        followingPath=false;

    }


    private void FollowPathToTarget()
    {
        
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

            followingPath = false;
            StopFollowingPath();
            return;
        }
    }

    private void StopFollowingPath()
    {
        rb.velocity = Vector2.zero;
        RefreshCurrentPath();
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
