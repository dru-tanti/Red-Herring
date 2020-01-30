using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField]
    public IntVariable max_health;

    void OnTriggerStay2D(Collider2D collider)
    {
        //Player has two colliders - we only want to register the trigger
        if(collider.isTrigger){
            //Turn off the potion's collider until further notice
            this.GetComponent<Collider2D>().enabled = false;
            
            if (collider.tag == "Player"){           
                if (collider.GetComponent<PlayerHealth>().health.Value < max_health.Value){
                    //If player needs healing, destroy the potion and add 25 hp
                    Destroy(gameObject);
                    collider.GetComponent<PlayerHealth>().health.Value += 25;
                }else{
                    //If player is at full health, turn the potion collider back on
                    this.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }
}
