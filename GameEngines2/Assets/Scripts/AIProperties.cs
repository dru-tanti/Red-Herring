using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Will define what certain enemies can do.
//-------------------------------------------------------------------------------------------------------------------------- 

[CreateAssetMenu(menuName = "Game/AI Behaviour", fileName = "AI Behaviour")]
public class AIProperties : ScriptableObject
{
    // SerializeField will make it appear in the inspector. 
    // Public will not appear in the inspector by default
    [SerializeField] private bool _canJump = false;
    public bool canJump { get => _canJump; }

    [SerializeField] private float _moveSpeed = 2f;
    public float moveSpeed { get => _moveSpeed; }
}
