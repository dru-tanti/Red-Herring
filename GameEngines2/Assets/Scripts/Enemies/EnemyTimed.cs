using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimed : MonoBehaviour
{
    private PlayerControl _player;
    public float timer;

    void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.tag.Equals ("Player")) 
        {
            Invoke("DamageDeal", timer);
        }    
    }
    void DamageDeal()
    {
        PlayerControl _player = GetComponent<Collider>().gameObject.GetComponent<PlayerControl>();
        _player.killPlayer();
    }
}
