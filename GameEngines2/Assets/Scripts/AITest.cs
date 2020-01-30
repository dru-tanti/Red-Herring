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
    public IntVariable coins;

    [SerializeField]
    public GameObject health_bar;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("I LIVE!");
    }

    //Damaging player on contact - testing player health bar
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Player"){
            collider.GetComponent<PlayerHealth>().health.Value -= 50;
        }
    }

    //Damaging enemy on hit - testing enemy health bar
    public void EnemyIsHit(int health){
        health_bar.SetActive(true);
        GameObject pool = GameObject.Find("HealthBar/Pool");
        pool.transform.localScale = new Vector2(1.0f * health / AIBehaviour.enemyMaxHealth , pool.transform.localScale.y);

        if(health == 0){
            Destroy(gameObject);
            coins.Value += 15;
        }
    }

}
