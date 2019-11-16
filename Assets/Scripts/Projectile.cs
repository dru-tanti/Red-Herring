﻿using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    // Controls the lasers' speed.
    public float speed = 10f;

    // The laser's lifetime.
    public float lifetime = 2f;

    // A reference to the laser's rigidbody.
    private Rigidbody2D _projectileRB;
    public IntVariable selectedElement;

    private void Awake()
    {
        _projectileRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Fire the projectile horizontally in relation to the spawn point.
        _projectileRB.velocity = transform.TransformDirection(Vector2.right) * speed;
        
        // Destroy this object after a specific amount of time.
        Destroy(gameObject, lifetime);
    }
}
