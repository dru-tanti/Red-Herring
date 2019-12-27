using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Game/Tiles/Water")]
public class WaterTile : Tile {
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Something Touched!");
        if(other.gameObject.tag == "Player"){
            Debug.Log("You are drowning!");
        }
    }
}
