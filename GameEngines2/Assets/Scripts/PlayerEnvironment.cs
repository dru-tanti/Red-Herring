using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

//--------------------------------------------------------------------------------------------------------------------------
// Gets the tile that the player is currently touching or aiming at.
// It will also control what effects the terrain will have on the player and vice versa.
//--------------------------------------------------------------------------------------------------------------------------
public class PlayerEnvironment : MonoBehaviour {
    [Header("Player References")]
    // The radius at which we will detect the cells around the player.
    [SerializeField] private float _radius = 1f;
    public float radius { get => _radius; }

    // Stores the transform behind the player so that we can later restore a dug tile.
    public Vector3 playerBack { get => transform.TransformPoint(Vector3.left * radius); }
    public Vector3Int backCell { get => TilemapManager.current.tilemap.WorldToCell(playerBack); }

    // Stores the transform above the player so that we can later restore a dug tile.
    public Vector3 playerTop { get => transform.TransformPoint(Vector3.up * radius); }
    public Vector3Int topCell { get => TilemapManager.current.tilemap.WorldToCell(playerTop); }

    public Transform shotPoint;
    public Transform groundCheck;
    private PlayerControl _player;

    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;

    private string lastActiveScene = null; // Used to check if the scene has changed.
    [SerializeField] private StringVariable currentActiveScene = null;

    private void Awake() {
        _player = GetComponent<PlayerControl>();
        lastActiveScene = currentActiveScene.Value;
    }
    
    void Update() {
        // Finds the cell that is currently occupied by the shotPoint.
        Vector3Int cellAim = TilemapManager.current.grid.WorldToCell(shotPoint.position);
        // Finds the tile in the tilemap that is currently in the cell retrieved above.
        TileBase tileAim = TilemapManager.current.tilemap.GetTile(cellAim);

        // Finds the cell that is currently occupied by the groundCheck.
        Vector3Int cellStand = TilemapManager.current.grid.WorldToCell(groundCheck.position);
        TileBase tileStand = TilemapManager.current.tilemap.GetTile(cellStand);

        // // Finds and stores th cell just above the player.
        // Vector3Int cellTop = grid.WorldToCell(playerTop);
        // TileBase tileTop = tilemap.GetTile(cellTop);

        // // Finds and stores the cell just behind the player.
        // Vector3Int cellBack = grid.WorldToCell(playerBack);
        // TileBase tileBack = tilemap.GetTile(cellBack);

        // Replace any dug tiles above or behind the player.
        // UnDig(cellBack, tileBack, cellTop, tileTop);

        // Only use the secondary abilites when the "C" button is pressed.
        if(Input.GetKeyDown(KeyCode.C) && element[selectedElement.Value] != null) {
            if(_player.cooldowns[selectedElement.Value].abilityAvailable[1].Value) {
                foreach(ElementEffect otherEffects in element[selectedElement.Value].otherEffects) {
                    UseEffect(otherEffects, cellAim, tileAim);
                }
            } else {
                Debug.Log("Ability not yet available");
                return;
            }
        }

        // Passive abilities should always be active, depending on the current selected element.
        foreach(ElementEffect passiveEffects in element[selectedElement.Value].passiveEffects) {
            UseEffect(passiveEffects, cellAim, tileAim);
        }

        // if (tileAim == null) return;
        // if (tileStand == null) return;

        // If the player is currently standing on a hazard, deal damage.
        if (tileStand is SpikeTile) {
            Debug.Log("Ouch");
        }

    }

    private void UseEffect(ElementEffect effect, Vector3Int cellAim, TileBase tileAim) {
        if(effect.willDig) {
            Dig(cellAim, tileAim);
        }
    }

    // If the player is aiming at a diggable tile, replace it with a dug tile.
    public void Dig(Vector3Int cellAim, TileBase tileAim) {
        if (tileAim is GroundTile && (tileAim as GroundTile).Dug == true) return;
        if (tileAim is GroundTile && (tileAim as GroundTile).Dug == false) {
            TilemapManager.current.tilemap.SetTile(cellAim, (tileAim as GroundTile).dugVersion);
        }
    }
    
    // If the tile is already dug, set it back to an undug tile when the player gets past it.
    // public void UnDig(Vector3Int cellBack, TileBase tileBack, Vector3Int cellTop, TileBase tileTop) {
    //     if(tileBack is GroundTile && (tileBack as GroundTile).Dug == true) {
    //         Debug.Log("We Touched the Back!");
    //         tilemap.SetTile(cellBack, (tileBack as GroundTile).dugVersion);
    //     }
            
    //     if(tileTop is GroundTile && (tileTop as GroundTile).Dug == true) {
    //         Debug.Log("We Touched the Top!");
    //         tilemap.SetTile(cellTop, (tileTop as GroundTile).dugVersion);
    //     }  
    // }
}
