using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField] private BoolVariable abilityAvailable = null;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            abilityAvailable.Value = true;
            Debug.Log("Element Unlocked! - " + abilityAvailable.name);
        }
    }
}
