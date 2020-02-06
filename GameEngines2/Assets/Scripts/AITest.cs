using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// NOTE: Used for testing purposes only.
//--------------------------------------------------------------------------------------------------------------------------

public class AITest : AIBehaviour
{
    [SerializeField]
    public GameObject health_bar;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("I LIVE!");
    }

    //Damaging enemy on hit - testing enemy health bar
    public void EnemyIsHit(int health){
        health_bar.SetActive(true);
        GameObject pool = GameObject.Find("HealthBar/Pool");
        pool.transform.localScale = new Vector2(1.0f * health / AIBehaviour.enemyMaxHealth , pool.transform.localScale.y);

        if(health == 0){
            Destroy(gameObject);
        }
    }

}
