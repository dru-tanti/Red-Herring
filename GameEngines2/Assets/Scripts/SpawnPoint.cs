using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class SpawnPoint : MonoBehaviour { 
    [SerializeField] private string _spawnName = null;
    public string spawnName { get => _spawnName; }
    [SerializeField] private StringConstant _sceneName = null;
    public StringConstant sceneName { get => _sceneName; }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            PlayerSpawner.current.setCurrentSpawn(this.spawnName, sceneName.Value);
        }
    }
}