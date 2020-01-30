using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        //Player has two colliders - we only want to register the trigger
        if(collider.isTrigger){
                
            if (collider.tag == "Player"){           
                Destroy(gameObject);
                //Grants the player 1 coin
                collider.GetComponent<PlayerKeyRing>().keys.Value += 1;
            }
        }
    }
}
