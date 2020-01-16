using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Base class for the player controller and the enemy controller. Contains properties shared by both.
//--------------------------------------------------------------------------------------------------------------------------

[RequireComponent(typeof(Rigidbody2D))]
public class BaseController : MonoBehaviour {

    protected Rigidbody2D _rb;
    protected float _gravityScale; // Keeps a reference of the default value to use later.

    protected virtual void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _gravityScale = _rb.gravityScale;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            Debug.Log("Hi");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            Debug.Log("Bye");
        }
    }
    
    // Used to change the gravity scale.
    protected void Gravity(float multiplier) {
        _rb.gravityScale = _gravityScale * multiplier;
    }
}
