// @author: Andrew Tanti

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
    [HideInInspector] public Vector3Int cellPlayer;
    [HideInInspector] public TileBase tilePlayer;

    public Transform shotPoint;
    public Transform groundCheck;
    private PlayerControl _player;
    private float _moveY; // To be used when the player is swimming
    public bool isTouchingWall, inWater;

    private void Awake() {
        _player = GetComponent<PlayerControl>();
    }
    
    void Update() {
        cellPlayer = TilemapManager.current.grid.WorldToCell(_player.transform.position);
        tilePlayer = TilemapManager.current.tilemap.GetTile(cellPlayer);

        // Finds the cell that is currently occupied by the shotPoint.
        cellAim = TilemapManager.current.grid.WorldToCell(shotPoint.position);
        // Finds the tile in the tilemap that is currently in the cell retrieved above.
        tileAim = TilemapManager.current.tilemap.GetTile(cellAim);

        // Finds the cell that is currently occupied by the groundCheck.
        cellStand = TilemapManager.current.grid.WorldToCell(groundCheck.position);
        tileStand = TilemapManager.current.tilemap.GetTile(cellStand);

        isTouchingWall = (tileAim is GroundTile);

        Debug.Log(TilemapManager.current.tilemap.GetTile(cellAim));
        // If the player is currently standing on a hazard, deal damage.
        if ((tileStand is HazardTile || tilePlayer is HazardTile) && !(tileStand as HazardTile).lava) {
            _player.killPlayer();
        }

        if(tileStand is HazardTile && (tileStand as HazardTile).lava && !_player._lavaResistant.Value) {
            _player.killPlayer();
        }

        // If the player is in the water, but they are not using Water, kill the player.
        if(tilePlayer is WaterTile && !_player._swimming.Value) {
            _player.killPlayer();
        }

        if((tilePlayer is WaterTile || tileStand is WaterTile) && _player._swimming.Value) {
            inWater = true;
            _player.Gravity(0.8f);
            _moveY = Input.GetAxisRaw("Vertical");
            _player._rb.velocity = new Vector2 (_player._rb.velocity.x, _moveY * _player.speed.Value);
        } 

        // If the player is no longer in the water but isWater is true, reset the settings.
        if(!(tilePlayer is WaterTile || tileStand is WaterTile) && inWater) {
            _player.Gravity(1f);
            inWater = false;
        }

        if(tileStand is CloudTile && (tileStand as CloudTile).walkable == true) {
            StartCoroutine(TilemapManager.current.startStorm(cellStand, 2, 2));
        }
    }
}
