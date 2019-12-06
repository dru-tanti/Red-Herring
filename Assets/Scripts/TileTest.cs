using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour
{
    public Tilemap tilemap;

    public Grid grid;

    [SerializeField] private float _radius = 1f;
    public float radius { get => _radius; }

    public Vector3 playerBack
        { get => transform.TransformPoint(Vector3.left * radius); }

    public Vector3Int backCell
        { get => tilemap.WorldToCell(playerBack); }

    public Transform groundCheck;

    void Update()
    {
        Vector3Int cell = grid.WorldToCell(groundCheck.position);
        // Vector3Int cell = grid.WorldToCell(groundCheck.position);
        TileBase tile = tilemap.GetTile(cell);

        if (tile == null) return;

        if (tile is SpikeTile)
        {
            Debug.Log("Ouch");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (tile is GroundTile)
            {
                tilemap.SetTile(cell, (tile as GroundTile).dugVersion);
            }
        }
    }
}
