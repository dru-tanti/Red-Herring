using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
  public float speed;
  public float distance;

  private bool movingRight = true;

  public Transform groundDetection;

 // Makes Enemy Patrol: If the groundDection does not detect ground, the enemy will flip and move the other way
  void FixedUpdate()
  {
    transform.Translate(Vector2.right * speed * Time.deltaTime);
    RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
     if(groundInfo.collider == false){
         if(movingRight == true){
             transform.eulerAngles = new Vector3(0, -180, 0);
             movingRight = false;
         } else {
             transform.eulerAngles = new Vector3(0, 0, 0);
             movingRight = true;
         }
        }
 }
  
// If enemy collides with player, it deals damage (Debug Log is displayed for now)
 void OnTriggerEnter2D(Collider2D collider)
 {
     if(collider.tag == "Player")
     {
         Debug.Log("Dealing Damage");
     }
 }
}
