using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public class TileSettings
    {
        public Color color;

        public Tile tile;
    }

    [SerializeField] private Texture2D _texture;

    [SerializeField] private Tilemap _tilemap;

    [SerializeField] private TileSettings[] _tileSettings;

    [SerializeField] private Vector2Int _startPixel, _generateSize;

    [SerializeField] private bool _clearOnGenerate = true;

    #if UNITY_EDITOR
    public void Clear()
    {
        _tilemap.ClearAllTiles();
    }

    public void GenerateMap()
    {
        if (_texture == null)
        {
            Debug.LogError("No texture was found!");
            return;
        }

        if (_clearOnGenerate) Clear();

        int width = _texture.width;
        int height = _texture.height;

        int offsetX = width / 2;
        int offsetY = height / 2;

        for (int x = _startPixel.x; x < _startPixel.x + _generateSize.x; x++)
        {
            for (int y = _startPixel.y; y < _startPixel.y + _generateSize.y; y++)
            {
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