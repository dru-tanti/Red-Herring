using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyKnockback : MonoBehaviour
{
    public float pushForce;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerControl playerPush = other.gameObject.GetComponent<PlayerControl>();

            Vector3 offset = transform.position - other.transform.position;
            playerPush.Push(Mathf.Sign(offset.x), pushForce);

        }
    }
}
