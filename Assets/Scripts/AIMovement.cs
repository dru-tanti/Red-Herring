using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private AIBehaviour _behaviour;

    // Start is called before the first frame update
    void Start()
    {
        if (_behaviour.canJump)
        {
            Debug.Log("I am Jumping!");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
