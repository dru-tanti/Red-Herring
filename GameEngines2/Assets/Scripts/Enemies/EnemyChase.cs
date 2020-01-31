using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChase : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDist = 3f;

    public Transform enemy;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath =  false;
    Seeker seeker;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(_rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            } else {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            _rb.AddForce(force);

            float distance = Vector2.Distance(_rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDist)
            {
                currentWaypoint++;
            }

            if (_rb.velocity.x >= 0.01f)
            {
                enemy.localScale = new Vector3(-1f, 1f, 1f);
            } else if (_rb.velocity.x <= -0.01f)
            {
                enemy.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
      if(other.gameObject.tag == "Player")
      {
          Debug.Log("Dealing Damage");
      }   
    }
}
