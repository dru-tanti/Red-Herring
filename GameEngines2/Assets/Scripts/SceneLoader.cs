﻿// @author: Andrew Tanti

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms;

//--------------------------------------------------------------------------------------------------------------------------
// Attached to the triggers that will control the loading and unloading of scenes.
//--------------------------------------------------------------------------------------------------------------------------
public class SceneLoader : MonoBehaviour {
    [Tooltip("The Scene (if any) that will Load/Unload Above the Trigger")]
    [SerializeField] private StringConstant sceneNameUp = null;
    [Tooltip("The Scene (if any) that will Load/Unload Below the Trigger")]
    [SerializeField] private StringConstant sceneNameDown = null;
    [Tooltip("The Scene (if any) that will Load/Unload to the Right of the Trigger")]
    [SerializeField] private StringConstant sceneNameRight = null;
    [Tooltip("The Scene (if any) that will Load/Unload to the Left of the Trigger")]
    [SerializeField] private StringConstant sceneNameLeft = null;
    [SerializeField] private StringVariable activeScene = null;
  
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            // Checks the difference between the players position and the trigger to determine the direction the player entered from.
            Vector3 offset = transform.position - other.transform.position;
            if(sceneNameRight || sceneNameLeft) {
                Debug.Log("X is " + Mathf.Sign(offset.x));
                switch(Mathf.Sign(offset.x)) {
                    case -1: 
                        if(!sceneNameLeft) return;
                        // if the scene we want to load is already loaded, return.
                        if(SceneManager.GetSceneByName(sceneNameLeft.Value).isLoaded) return;
                        Debug.Log("Loading Scene" + sceneNameLeft.Value);
                        SceneManager.LoadScene(sceneNameLeft.Value, LoadSceneMode.Additive);        
                        break;
                    case 1:
                        if(!sceneNameRight) return;
                        // if the scene we want to load is already loaded, return.
                        if(SceneManager.GetSceneByName(sceneNameRight.Value).isLoaded) return;
                        Debug.Log("Loading Scene" + sceneNameRight.Value);
                        SceneManager.LoadScene(sceneNameRight.Value, LoadSceneMode.Additive);      
                        break;
                }
            }
            if(sceneNameUp || sceneNameDown) {
                Debug.Log("Y is " + Mathf.Sign(offset.x));
                switch(Mathf.Sign(offset.y)) {
                    case -1: 
                        if(!sceneNameDown) return;
                        // if the scene we want to load is already loaded, return.
                        if(SceneManager.GetSceneByName(sceneNameDown.Value).isLoaded) return;
                        Debug.Log("Loading Scene" + sceneNameDown.Value);
                        SceneManager.LoadScene(sceneNameDown.Value, LoadSceneMode.Additive);        
                        break;
                    case 1:
                        if(!sceneNameUp) return;
                        // if the scene we want to load is already loaded, return.
                        if(SceneManager.GetSceneByName(sceneNameUp.Value).isLoaded) return;
                        Debug.Log("Loading Scene" + sceneNameUp.Value);
                        SceneManager.LoadScene(sceneNameUp.Value, LoadSceneMode.Additive);      
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            // Checks the difference between the players position and the trigger to determine the direction the player entered from.
            Vector3 offset = transform.position - other.transform.position;
            // If a scene is not set on either the left or right, then we can skip this.
            GameObject camera = GameObject.FindGameObjectWithTag("Camera");
            if(sceneNameRight || sceneNameLeft) {
                Debug.Log("Exit X is " + Mathf.Sign(offset.x));
                switch(Mathf.Sign(offset.x)) {
                    case -1: 
                        // if a Scene is not set return.
                        if(!sceneNameRight) return;
                        // if scene is the current active scene, we do not want to unload it. So return.
                        // if(sceneNameRight.Value == activeScene.Value) return;
                        SceneManager.MoveGameObjectToScene(camera, SceneManager.GetSceneByName(sceneNameRight.Value));
                        SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByName(sceneNameRight.Value));
                        activeScene.Value = sceneNameRight.Value;
                        // SceneManager.UnloadSceneAsync(sceneNameLeft.Value);                         
                        break;
                    case 1:
                        if(!sceneNameLeft) return;
                        // if(sceneNameLeft.Value == activeScene.Value) return;
                        SceneManager.MoveGameObjectToScene(camera, SceneManager.GetSceneByName(sceneNameLeft.Value));
                        SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByName(sceneNameLeft.Value));
                        activeScene.Value = sceneNameLeft.Value;
                        // SceneManager.UnloadSceneAsync(sceneNameRight.Value);
                        break;
                }
            }
            if(sceneNameUp || sceneNameDown) {
                Debug.Log("Exit Y is " + Mathf.Sign(offset.x));
                switch(Mathf.Sign(offset.y)) {
                    case -1: 
                        if(!sceneNameUp) return;
                        // if(sceneNameUp.Value == activeScene.Value) return;
                        SceneManager.MoveGameObjectToScene(camera, SceneManager.GetSceneByName(sceneNameUp.Value));
                        SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByName(sceneNameUp.Value));
                        activeScene.Value = sceneNameUp.Value;
                        // SceneManager.UnloadSceneAsync(sceneNameDown.Value); 
                        break;
                    case 1:
                        if(!sceneNameDown) return;
                        // if(sceneNameDown.Value == activeScene.Value) return;
                        SceneManager.MoveGameObjectToScene(camera, SceneManager.GetSceneByName(sceneNameDown.Value));
                        SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByName(sceneNameDown.Value));
                        activeScene.Value = sceneNameDown.Value;
                        // SceneManager.UnloadSceneAsync(sceneNameUp.Value);   
                        break;
                }
            }
        }
    }
}
