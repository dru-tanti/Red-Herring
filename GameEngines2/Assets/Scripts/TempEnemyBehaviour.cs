using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player"){
            collider.GetComponent<PlayerHealth>().health.Value -= 50;
        }
    }
}
