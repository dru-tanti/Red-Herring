using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimed : MonoBehaviour
{
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
        //Animation Change
        Debug.Log("Dealing Damage");
    }
}
