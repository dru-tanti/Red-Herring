using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Game/Tiles/Invisible")]
public class InvisibleTile : Tile {
    private void Start() {
        this.color = new Color (1f, 1f, 1f, 0.5f);
    }
}
