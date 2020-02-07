using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour

{
    private PlayerControl _player;

    private void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            PlayerControl _player = collider.gameObject.GetComponent<PlayerControl>();
            _player.killPlayer();
            Debug.Log("Dealing Damage");
        }
    }
}