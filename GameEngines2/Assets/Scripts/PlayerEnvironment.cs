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
    private float _moveY;
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

        isTouchingWall = (tileAim is GroundTile) ? true : false;

        // If the player is currently standing on a hazard, deal damage.
        if (tileStand is HazardTile && !(tileStand as HazardTile).lava) {
            Debug.Log("Ouch");
        }

        if(tileStand is HazardTile && (tileStand as HazardTile).lava) {
            Debug.Log("You are drowning in Lava");
        }

        if(tilePlayer is WaterTile || tileStand is WaterTile) {
            inWater = true;
            if(_player._swimming.Value == true) {
                _player.Gravity(0.8f);
                _moveY = Input.GetAxisRaw("Vertical");
                _player._rb.velocity = new Vector2 (_player._rb.velocity.x, _moveY * _player.speed.Value);
            } else {
                Debug.Log("You have drowned");
            }
        } 

        // If the player is no longer in the water but isWater is true, reset the settings.
        if(!(tilePlayer is WaterTile || tileStand is WaterTile) && inWater) {
            _player.Gravity(1f);
            inWater = false;
        }

        if(tileStand is CloudTile && (tileStand as CloudTile).walkable == true) {
            StartCoroutine(TilemapManager.current.startStrom(cellStand, 2, 2));
        }
    }
}
