using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

//--------------------------------------------------------------------------------------------------------------------------
// Gets a reference to the Grid and Tilemap in the current scene.
// Will also handle abilities that need access to the Tilemap.
//--------------------------------------------------------------------------------------------------------------------------
public class TilemapManager : MonoBehaviour {
    public static TilemapManager current;

    [Header("Tilemap")]
    public Tilemap tilemap;
    public Grid grid;

    // Tiles that will be swapped when the player becomes invisible
    [SerializeField] private InvisibleTile visible = null;
    [SerializeField] private InvisibleTile invisible = null;
    [SerializeField] private BoolVariable _isInvisible = null;

    private string lastActiveScene = null; // Used to check if the scene has changed.
    [SerializeField] private StringVariable currentActiveScene = null;

    private void Awake() {
        if (current == null) {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
            return;
        }

        // If the grid or tilemap are not set, find the correct gameobjects.
        if(!grid) grid = GameObject.Find("Grid-"+currentActiveScene.Value).GetComponent<Grid>();
        if(!tilemap) tilemap = GameObject.Find("Tilemap-"+currentActiveScene.Value).GetComponent<Tilemap>();
        lastActiveScene = currentActiveScene.Value;
    }
    void Update() {
        // If the scene changes, find the correct Grid and Tilemap.
        if(lastActiveScene != currentActiveScene.Value) {
            grid = GameObject.Find("Grid-"+currentActiveScene.Value).GetComponent<Grid>();
            tilemap = GameObject.Find("Tilemap-"+currentActiveScene.Value).GetComponent<Tilemap>();
            lastActiveScene = currentActiveScene.Value;
        }
    }
    public void showHiddenTiles() {
        Tile changeTile = (_isInvisible.Value == true) ? invisible : visible;
        Tile newTile = (_isInvisible.Value == true) ? visible: invisible;
        tilemap.SwapTile(changeTile, newTile);
    }
}
