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
    [SerializeField] private BoolVariable _isInvisible = null;

    [Header("Invisible Tiles")]
    [SerializeField] private InvisibleTile visible = null;
    [SerializeField] private InvisibleTile invisible = null;

    [Header("Cloud Tiles")]
    [SerializeField] private CloudTile _notWalkable = null;
    [SerializeField] private CloudTile _walkable = null;
    [SerializeField] private CloudTile _stormy = null;

    public IntVariable selectedElement;
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

    // public void destroyTile(Vector3Int[] tiles) {
    //     for(int i = 1; i < tiles.Length; i++) {
    //         Debug.Log(tilemap.GetTile(tiles[i]));
    //         // if(tiles[i] is BreakableTile) {
    //         //     tilemap.SetTile(tiles[i], null);
    //         // }
    //     }
    // }

    public void setCloudWalkable() {
        Tile changeTile = (selectedElement.Value == 0) ? _notWalkable : _walkable;
        Tile newTile = (selectedElement.Value == 0) ? _walkable : _notWalkable;
        tilemap.SwapTile(changeTile, newTile);
    }

    public IEnumerator startStrom(Vector3Int position, float changeTime, float activeTime) {
        yield return new WaitForSeconds(changeTime);
        tilemap.SetTile(position, _stormy);
        yield return new WaitForSeconds(activeTime);
        if(selectedElement.Value == 0) {
            tilemap.SetTile(position, _walkable);
        } else {
            tilemap.SetTile(position, _notWalkable);
        }
    }

    // public IEnumerator freezeTile(Vector3Int position, float activeTime) {
    //     TileBase tiletmp = tilemap.GetTile(cellGround);
    //     tilemap.SetTile(position, _frozen);
    //     yield return new WaitForSeconds(activeTime);
    //     tilemap.SetTile(position, tiletmp);
    // }

    public void findGrid() {
        grid = GameObject.Find("Grid-"+currentActiveScene.Value).GetComponent<Grid>();
        tilemap = GameObject.Find("Tilemap-"+currentActiveScene.Value).GetComponent<Tilemap>();
    }

    public void showHiddenTiles() {
        Tile changeTile = (_isInvisible.Value == true) ? invisible : visible;
        Tile newTile = (_isInvisible.Value == true) ? visible: invisible;
        tilemap.SwapTile(changeTile, newTile);
    }
}
