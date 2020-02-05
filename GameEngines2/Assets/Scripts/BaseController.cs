// @author: Andrew Tanti

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Base class for the player controller and the enemy controller. Contains properties shared by both.
//--------------------------------------------------------------------------------------------------------------------------

[RequireComponent(typeof(Rigidbody2D))]
public class BaseController : MonoBehaviour {

    [HideInInspector] public Rigidbody2D _rb;
    protected float _gravityScale; // Keeps a reference of the default value to use later.

    protected virtual void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _gravityScale = _rb.gravityScale;
    }
    
    // Used to change the gravity scale.
    public void Gravity(float multiplier) {
        _rb.gravityScale = _gravityScale * multiplier;
    }
}
