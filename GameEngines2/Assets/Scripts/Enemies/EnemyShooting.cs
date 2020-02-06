using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private bool isAttacking = false;
    public float speed;
    public float time;
    public float repeat;
    private bool moveRight = true;
    public Transform patrolStart;
    public Transform patrolEnd;
    private Vector2 target;
    public Transform projectileSpawn;
    public GameObject Projectile;

    private void Start()
    {
        target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
        // InvokeRepeating("Shooting", 2.0f, 0.3f);
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
            if(moveRight)
            {   
                target = new Vector2(patrolStart.position.x, patrolStart.position.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                moveRight = false;
            } else {                   
                target = new Vector2(patrolEnd.position.x, patrolEnd.position.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
                moveRight = true;
            }
        }
    }
    // Once player enters the trigger, start the coroutine that shoots at the player
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
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

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            isAttacking = false;
        }    
    }
}
