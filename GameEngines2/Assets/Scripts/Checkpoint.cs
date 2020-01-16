using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Checkpoint", menuName = "GameEngines2/Checkpoint", order = 0)]
public class Checkpoint : ScriptableObject {

    [SerializeField] private Sprite _sprite = null;
    public Sprite sprite { get => _sprite; }

    [SerializeField] private string _name = null;
    public string name { get => _name; }

    [SerializeField] private int _sceneIndex = 0;
    public int sceneIndex { get => _sceneIndex; }

 

    // * Check if particle system will be needed.
    // [SerialiseField] private SpawnDecoration _spawn;
    // public int spawn { get => _spawn; }
}
