﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour {
    public static PlayerSpawner current { get; private set; }
    [SerializeField] private PlayerControl _playerPrefab = null;
    private SpawnPoint[] _spawnPoints = null;
    public StringVariable currentSpawn = null;
    public StringVariable currentSpawnScene = null;
    public StringVariable activeScene = null;
    public BoolVariable _isAlive = null;
    private void Awake() {
        if (current == null) {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }

        SceneManager.sceneLoaded += CheckPlayer;
    }

    void CheckPlayer(Scene scene, LoadSceneMode sceneMode) {
        if(GameObject.FindGameObjectsWithTag("Player").Length == 0) {
            Debug.Log("No Player Found! Respawning");
            spawnPlayer();
        }
    }

    public void setCurrentSpawn(string spawnName, string sceneName) {
        currentSpawn.Value = spawnName;
        currentSpawnScene.Value = sceneName;
    }

    /* 
        1) Load the scene where the current spawn is located.
        2) Once scene is loaded find all the spawn points found in the scene.
        3) Find the spawn that is set as the current spawn point.
        4) Spawn the player.
    */
    public void spawnPlayer() {
        if(!SceneManager.GetSceneByName(currentSpawnScene.Value).isLoaded){
            Debug.Log("Loading Scene");
            SceneManager.LoadScene(currentSpawnScene.Value);
            activeScene.Value = currentSpawnScene.Value;
        }
        Debug.Log("Finding Spawn Points");
        _spawnPoints = FindObjectsOfType<SpawnPoint>();
        Debug.Log(_spawnPoints[0]);
        foreach(SpawnPoint spawn in _spawnPoints) {
            if(spawn.spawnName == currentSpawn.Value){
                Debug.Log("Spawn Point found, respawning player");
                Instantiate(_playerPrefab, new Vector3(spawn.transform.position.x, spawn.transform.position.y + 1f, 0f), Quaternion.identity);
            }
        }
    }
}
