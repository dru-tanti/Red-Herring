﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
[RequireComponent (typeof (Transform))]
public class NewChase : MonoBehaviour
{
    [Header ("Chase AI Variables")]

    // TODO: Add platforming Capability
    public Transform target;
    // How many times we will update our path.
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D _enemyRigidbody;

    // The calculated path
    public Path path;

    [Header ("Enemy Movement Variables")]
    // The speed of the AI
    public float speed = 5f;
    public float stoppingDistance;
    public float retreatDistance;

    // [Header ("Emeny Attack Variables")]
    // private float timeBtwShots;
    // public float startTimeBtwShots;
    // public float range;
    // public GameObject Laser;

     public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    /* 
        The maximum distace from the AI to a waypoint 
        for it to continue to the next waypoint.
    */ 
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards.
    private int currentWaypoint;

    private void Start() 
    {
        seeker = GetComponent<Seeker>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        // timeBtwShots = startTimeBtwShots;
        Physics2D.queriesStartInColliders = false;

        if (target == null)
        {
            Debug.LogError("No Player Found!");
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete (Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            // TODO: Target a player search here.
            yield return false;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds (1/updateRate);

        // It will continuously call itself
        StartCoroutine(UpdatePath());
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            Debug.Log("Target is Null");
            return;
        }

        if(path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            if(pathIsEnded)
                return;

            Debug.Log("End of path reached.");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        // Finding direction to the next waypoint.
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        // Chase Enemy Attack using Dir to get the direction it should aim.
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dir);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if(hitInfo.collider.tag == "Player")
            {
                Debug.Log("Player Found");
                Death();
            } else {
                Debug.DrawLine(transform.position, transform.position + dir, Color.green);
            }
        }
            

        dir *= speed * Time.fixedDeltaTime;

        // Move the enemy
        _enemyRigidbody.AddForce(dir, fMode);

        float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
        if(dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

        
    }

    private void Death()
    {
        Debug.Log("Player is Dead");
    }
}