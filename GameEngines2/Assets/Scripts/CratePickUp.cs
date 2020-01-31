﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickUp : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        //Player has two colliders - we only want to register the trigger
        if(collider.isTrigger){
                
            if (collider.tag == "Player"){           
                Destroy(gameObject);
                //Grants the player 10 coins
                collider.GetComponent<PlayerWallet>().coins.Value += 10;
            }
        }
    }
}