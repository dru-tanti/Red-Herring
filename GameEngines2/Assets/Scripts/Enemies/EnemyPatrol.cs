using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public bool shoot = false;
    private bool isAttacking = false;
    public float speed;
    [Range(0f, 10f)]
    public float time;
    private bool moveRight = true;
    public Transform patrolStart;
    public Transform patrolEnd;
    private Vector2 patrolTarget;
    public Transform projectileSpawn;
    public GameObject Projectile;


    private void Start()
    {
        patrolTarget = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
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
        transform.position = Vector2.MoveTowards(transform.position, patrolTarget, speed * Time.deltaTime);
        if (transform.position.x == patrolTarget.x && transform.position.y == patrolTarget.y)
        {
            if(moveRight)
                {
                    patrolTarget = new Vector2(patrolStart.position.x, patrolStart.position.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    moveRight = false;
                } else {
                    patrolTarget = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    moveRight = true;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.tag == "Player" && !shoot)
        {
            Debug.Log("Charging");
            Vector2 playerpos = collider.transform.position;
            Debug.Log(playerpos);
            StartCoroutine(ChargePlayer(playerpos));
            //Debug.Log("Dealing Damage");
        }

        if(collider.gameObject.tag == "Player" && shoot) {
            isAttacking = true;
            StartCoroutine(Shooting());
        }
    }

    public IEnumerator Shooting() {
        while(isAttacking) {
            Instantiate(Projectile, projectileSpawn.position, projectileSpawn.rotation);
            yield return new WaitForSeconds(this.time);    
        }
    }
    private IEnumerator ChargePlayer(Vector2 playerPos) {
        bool charging = true;
        while(charging) {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, (speed * 2) * Time.deltaTime);
            yield return new WaitForSeconds(2f);
            charging = false;
        }
    }
    
    void OnCollisionEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isAttacking = false;
        }    
    }
}
