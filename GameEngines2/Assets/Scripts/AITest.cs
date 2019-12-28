using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Used for testing purposes only.
//--------------------------------------------------------------------------------------------------------------------------

public class AITest : AIBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I LIVE!");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Damaging player on contact - testing player health bar
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "Player"){
            collider.GetComponent<PlayerHealth>().health.Value -= 50;
        }
    }

    //Damaging enemy on hit - testing enemy health bar
    public void EnemyIsHit(int health){
        GameObject pool = GameObject.Find("Pool");
        pool.transform.localScale = new Vector2(1.0f * health / AIBehaviour.enemyMaxHealth , pool.transform.localScale.y);

        if(health == 0){
            Destroy(gameObject);
        }
    }
}
