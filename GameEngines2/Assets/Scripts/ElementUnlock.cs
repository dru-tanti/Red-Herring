using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUnlock : MonoBehaviour
{
    [SerializeField] private ElementType element = null;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            element.unlocked.Value = true;
            Debug.Log("Element Unlocked! - " + element.name);
        }
    }
}
