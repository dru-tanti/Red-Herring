// @author: Andrew Tanti

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

//---------------------------------------------------------------------------------------------
// Sets the information for this particular spawnpoint to be used later by PlayerSpawner
//---------------------------------------------------------------------------------------------
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