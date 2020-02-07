using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalPatrol : MonoBehaviour
{
    private bool isAttacking = false;
    public float speed;
    private bool moveUp = true;
    public Transform patrolStart;
    public Transform patrolEnd;
    private Vector2 target;


    private void Start()
    {
        target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
    }
    void Update()
    {
        // If enemy is not in attack mode, it will continue to patrol
        if(!isAttacking)
        {
            Patrol();
        }
    }
// Patrolling with points set in Unity
    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            if(moveUp)
                {
                    target = new Vector2(patrolStart.position.x, patrolStart.position.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    moveUp = false;
                } else {
                    target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    moveUp = true;
                }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Player")
     {
         Debug.Log("Dealing Damage");
     }
    }
}
