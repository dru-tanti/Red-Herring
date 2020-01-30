using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

//--------------------------------------------------------------------------------------------------------------------------
// Generates a Tilemap from an image that we will provide.
//--------------------------------------------------------------------------------------------------------------------------

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class TileSettings {
        public Color color; // What we will compare the current pixel to.
        public Tile tile; // What tile will the system place on that pixel.
    }

    [SerializeField] private Texture2D _texture = null; // The image we'll user to generate the map.
    [SerializeField] private Tilemap _tilemap = null;
    [SerializeField] private TileSettings[] _tileSettings = null;
    [SerializeField] private Vector2Int _startPixel, _generateSize;
    [SerializeField] private bool _clearOnGenerate = true; // If we will clear the tilemap everytime we generate or add to it.

    // Only the Unity Editor can access this part of the script.
    #if UNITY_EDITOR
    public void Clear() {
        _tilemap.ClearAllTiles();
    }

    public void GenerateMap() {
        if (_texture == null) {
            Debug.LogError("No texture was found!");
            return;
        }

        if (_clearOnGenerate) Clear();

        int width = _texture.width;
        int height = _texture.height;

        int offsetX = width / 2;
        int offsetY = height / 2;

        for (int x = _startPixel.x; x < _startPixel.x + _generateSize.x; x++) {
            for (int y = _startPixel.y; y < _startPixel.y + _generateSize.y; y++) {
                Color pixel = _texture.GetPixel(x, y);
                IEnumerable<TileSettings> selections = _tileSettings.Where(t => t.color == pixel);

                if (selections.Count() == 0) continue;

                Tile tileToGenerate = selections.ElementAt(0).tile;
                _tilemap.SetTile(new Vector3Int(x - offsetX, y - offsetY, 0), tileToGenerate);
            }
        }
    }
    #endif
}