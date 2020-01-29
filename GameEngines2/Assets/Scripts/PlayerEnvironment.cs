using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

//--------------------------------------------------------------------------------
// Gets the tile that the player is currently touching or aiming at.
//--------------------------------------------------------------------------------
public class PlayerEnvironment : MonoBehaviour {
    [Header("Player References")]
    [HideInInspector] public Vector3Int cellAim;
    [HideInInspector] public TileBase tileAim;
    [HideInInspector] public Vector3Int cellStand;
    [HideInInspector] public TileBase tileStand;

    public Transform shotPoint;
    public Transform groundCheck;
    private PlayerControl _player;

    private void Awake() {
        _player = GetComponent<PlayerControl>();
    }
    
    void Update() {
        // Finds the cell that is currently occupied by the shotPoint.
        cellAim = TilemapManager.current.grid.WorldToCell(shotPoint.position);
        // Finds the tile in the tilemap that is currently in the cell retrieved above.
        tileAim = TilemapManager.current.tilemap.GetTile(cellAim);

        // Finds the cell that is currently occupied by the groundCheck.
        cellStand = TilemapManager.current.grid.WorldToCell(groundCheck.position);
        tileStand = TilemapManager.current.tilemap.GetTile(cellStand);

        // If the player is currently standing on a hazard, deal damage.
        if (tileStand is HazardTile) {
            Debug.Log("Ouch");
        }

        if(tileStand is CloudTile && (tileStand as CloudTile).walkable == true) {
            StartCoroutine(TilemapManager.current.startStrom(cellStand, 2, 2));
        }
    }
}
