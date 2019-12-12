using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

//--------------------------------------------------------------------------------------------------------------------------
// Gets the tile that the player is currently touching or aiming at.
// It will also control what effects the terrain will have on the player and vice versa.
//--------------------------------------------------------------------------------------------------------------------------

public class TerrainControl : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap tilemap;
    public Grid grid;

    [Header("Player References")]
    // The radius at which we will detect the cells around the player.
    [SerializeField] private float _radius = 1f;
    public float radius { get => _radius; }

    // Stores the transform behind the player so that we can later restore a dug tile.
    public Vector3 playerBack
        { get => transform.TransformPoint(Vector3.left * radius); }
    public Vector3Int backCell
        { get => tilemap.WorldToCell(playerBack); }

    // Stores the transform above the player so that we can later restore a dug tile.
    public Vector3 playerTop
        { get => transform.TransformPoint(Vector3.up * radius); }
    public Vector3Int topCell
        { get => tilemap.WorldToCell(playerTop); }

    public Transform shotPoint;
    public Transform groundCheck;

    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;
    void Update()
    {
                // Finds the cell that is currently occupied by the shotPoint.
        Vector3Int cellAim = grid.WorldToCell(shotPoint.position);
        // Finds the tile in the tilemap that is currently in the cell retrieved above.
        TileBase tileAim = tilemap.GetTile(cellAim);

        // Finds the cell that is currently occupied by the groundCheck.
        Vector3Int cellStand = grid.WorldToCell(groundCheck.position);
        TileBase tileStand = tilemap.GetTile(cellStand);

        if(selectedElement.Value == 1)
        {
            // Finds and stores th cell just above the player.
            Vector3Int cellTop = grid.WorldToCell(playerTop);
            TileBase tileTop = tilemap.GetTile(cellTop);

            // Finds and stores the cell just behind the player.
            Vector3Int cellBack = grid.WorldToCell(playerBack);
            TileBase tileBack = tilemap.GetTile(cellBack);

            // If the tile is already dug, we will set it back to an undug tile when the player gets past it.
            if(tileBack is GroundTile && (tileBack as GroundTile).Dug == true)
            {
                Debug.Log("We Touched the Back!");
                tilemap.SetTile(cellBack, (tileBack as GroundTile).dugVersion);
            }
                
            if(tileTop is GroundTile && (tileTop as GroundTile).Dug == true)
            {
                Debug.Log("We Touched the Top!");
                tilemap.SetTile(cellTop, (tileTop as GroundTile).dugVersion);
            }  
            
            // If the payer is aiming towards a diggable tile, then we can dig through it.
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (tileAim is GroundTile && (tileAim as GroundTile).Dug == false)
                {
                    tilemap.SetTile(cellAim, (tileAim as GroundTile).dugVersion);
                }
            }

            if (tileAim == null) return;
            if (tileStand == null) return;
        }
        // If the player is currently standing on a hazard, deal damage.
        if (tileStand is SpikeTile)
        {
            Debug.Log("Ouch");
        }

    }
}
