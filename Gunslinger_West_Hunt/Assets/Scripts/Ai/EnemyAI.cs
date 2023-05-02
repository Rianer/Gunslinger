using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 20;
    public float nextWaypointDistance = 3;

    private Path path;
    private int currentWaypoint = 0;
    private bool followingPath;

    private Seeker seeker;
    private Rigidbody2D rb;


    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        followingPath = true;
        BuildPathToTarget();
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

    private void FixedUpdate()
    {
        if (followingPath) 
        {
            FollowPathToTarget();
        }
    }

    private void RefreshCurrentPath()
    {
        currentWaypoint = 0;
        followingPath=false;

    }


    private void FollowPathToTarget()
    {
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velocity = direction * speed;
        rb.velocity = velocity;
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {

            followingPath = false;
            StopFollowingPath();
            Debug.Log("Reached Endpoint");
            return;
        }
    }

    private void StopFollowingPath()
    {
        rb.velocity = Vector2.zero;
        RefreshCurrentPath();
    }

}
