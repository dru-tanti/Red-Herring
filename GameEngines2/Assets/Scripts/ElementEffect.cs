﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Contains all the possible effects of the elements.
// Instances of this will define what the element can do depending on what is selected.
//--------------------------------------------------------------------------------------------------------------------------

[CreateAssetMenu(menuName = "Game/Element Effect", fileName = "ElementEffect")]
public class ElementEffect : ScriptableObject
{
    [Header("Attack Effects")]
    [SerializeField] private bool _projectile = false;
    public bool projectile { get => _projectile; }

    [SerializeField] private bool _groundEffect = false;
    public bool groundEffect { get => _groundEffect; }

    [SerializeField] private bool _willDamage = false;
    public bool willDamage { get => _willDamage; }

    [Range(0.0f, 10.0f)]
    [SerializeField] private int _damage = 0;
    public int damage { get => _damage; }

    [SerializeField] private bool _turnInvisible = false;
    public bool turnInvisible { get => _turnInvisible; }
    
    [SerializeField] private bool _willPush = false;
    public bool willPush { get => _willPush; }
    
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _pushForce = 0;
    public float pushForce { get => _pushForce; }

    [SerializeField] private bool _willFreeze = false;
    public bool willFreeze { get => _willFreeze; }

    [SerializeField] private bool _willStun = false;
    public bool willStun { get => _willStun; }

    [Header("Other Effects")]
    [SerializeField] private bool _immune = false;
    public bool immune { get => _immune; }

    [SerializeField] private bool _fog = false;
    public bool fog { get => _fog; }

    [SerializeField] private bool _willFloat = false;
    public bool willFloat { get => _willFloat; }

    [Range(-5.0f, 0.0f)]
    [SerializeField] private float _floatSpeed = 0f;
    public float floatSpeed { get => _floatSpeed; }

    [SerializeField] private bool _willDig = false;
    public bool willDig { get => _willDig; }

    [SerializeField] private bool _willDash = false;
    public bool willDash { get => _willDash; }

    [Range(0.0f, 50.0f)]
    [SerializeField] private int _dashForce = 0;
    public int dashForce { get => _dashForce; }

    [Header("Passive Effects")]
    [SerializeField] private bool _resistance = false;
    public bool resistance { get => _resistance; }

    [SerializeField] private bool _wallJump = false;
    public bool wallJump { get => _wallJump; }

    [SerializeField] private bool _strength = false;
    public bool strength { get => _strength; }

    [SerializeField] private bool _swim = false;
    public bool swim { get => _swim; }
    
    [Header("Ability Active Time")]
    [Range(0f, 20.0f)]
    [SerializeField] private float _activeTime = 0f;
    public float activeTime { get => _activeTime; }

    [Header("Ability Cooldown")]
    [Range(0.0f, 10.0f)]
    [SerializeField] private float _cooldown = 0;
    public float cooldown { get => _cooldown; }
}
