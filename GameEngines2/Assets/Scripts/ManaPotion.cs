using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    [SerializeField]
    public IntVariable max_mana;

    void OnTriggerStay2D(Collider2D collider)
    {
        //Player has two colliders - we only want to register the trigger
        if(collider.isTrigger){
            //Turn off the potion's collider until further notice
            this.GetComponent<Collider2D>().enabled = false;
            
            if (collider.tag == "Player"){           
                if (collider.GetComponent<PlayerMana>().mana.Value < max_mana.Value){
                    //If player needs mana, destroy the potion and add 15 mp
                    Destroy(gameObject);
                    collider.GetComponent<PlayerMana>().mana.Value += 15;
                }else{
                    //If player is at full mana, turn the potion collider back on
                    this.GetComponent<Collider2D>().enabled = true;
                }
            }
        }
    }
}
