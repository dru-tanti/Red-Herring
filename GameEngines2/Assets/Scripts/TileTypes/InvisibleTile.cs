using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Game/Tiles/Invisible")]
public class InvisibleTile : Tile {
    public Sprite visible;
    public Sprite invisible;
    private bool isVisible = false;
    private Vector3Int tilePosition;
    private ITilemap tileMap;
    
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData) {
        base.GetTileData(location, tilemap, ref tileData);
        tilePosition = location;
        tileMap = tilemap;
    }

    public void turnVisible() {
        isVisible = !isVisible;
        this.sprite = (isVisible) ? visible : invisible;
    }
    
}
