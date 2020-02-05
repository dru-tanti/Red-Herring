// @author: Andrew Tanti

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//--------------------------------------------------------------------------------------------------------------------------
// Basic Camera controller for testing purposes.
//--------------------------------------------------------------------------------------------------------------------------

public class CameraController : MonoBehaviour {
    public static CameraController current { get; private set; }
    private CinemachineVirtualCamera _CM;

    private GameObject _player;
    private void Awake() {
        // Declares the camera as a singleton and makes sure that it is not destoyed on load.
        if (current == null) {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }

        // Retrieves a reference to the Cinemachine so that we can give it a reference to the player.
        _CM = GetComponent<CinemachineVirtualCamera>();
    }
    // Sets the player as the transfomr the Cinemachine will follow.
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _CM.m_Follow = _player.transform;
    }
    
    // If the player is destoryed, or the camera loses it's reference to it, find the player again.
    private void Update() {
        if(_player == null) {
            _player = GameObject.FindGameObjectWithTag("Player");
            _CM.m_Follow = _player.transform;
        }
    }
}
