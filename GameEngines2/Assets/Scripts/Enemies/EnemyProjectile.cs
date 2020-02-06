using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    private Transform player;
    private Rigidbody2D projectile;

    private void Awake()
    {
        projectile = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    private void Start()
        {
            Vector3 dir = (player.position - transform.position).normalized;
            projectile.velocity = transform.TransformDirection(dir) * speed;

            Destroy(gameObject, lifetime);
        }

    void OnTriggerEnter2D(Collider2D collider) 
        {
            if(collider.tag == "Player")
            {
                Debug.Log("Dealing Damage");
                Destroy(gameObject);
            }
        }   
}