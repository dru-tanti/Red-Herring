using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Contains all the possible effects of the elements.
// Instances of this will define what the element can do depending on what is selected.
//--------------------------------------------------------------------------------------------------------------------------

[CreateAssetMenu(menuName = "Game/Element Effect", fileName = "ElementEffect")]
public class ElementEffect : ScriptableObject
{
    [SerializeField] private bool _willDamage = false;
    public bool willDamage { get => _willDamage; }

    [SerializeField] private int _damage = 0;
    public int damage { get => _damage; }
    
    [SerializeField] private bool _willPush = false;
    public bool willPush { get => _willPush; }
    
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _pushForce = 0;
    public float pushForce { get => _pushForce; }

    [SerializeField] private bool _willFreeze = false;
    public bool willFreeze { get => _willFreeze; }

    [SerializeField] private int _freezeDuration = 0;
    public int freezeDuration { get => _freezeDuration; }

    [SerializeField] private bool _immune = false;
    public bool immune { get => _immune; }

    [SerializeField] private bool _fog = false;
    public bool fog { get => _fog; }

    [SerializeField] private bool _weightLess = false;
    public bool weightLess { get => _weightLess; }

    [SerializeField] private bool _willStun = false;
    public bool willStun { get => _willStun; }

    [SerializeField] private int _stunDuration = 0;
    public int stunDuration { get => _stunDuration; }

    [SerializeField] private bool _willDig = false;
    public bool willDig { get => _willDig; }
    
    [Range(0.0f, 10.0f)]
    [SerializeField] private float _cooldown = 0;
    public float cooldown { get => _cooldown; }
}
