using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int _width;
    private int _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private PowerNode _powerNodePrefab;

    private Dictionary<Vector2, Tile> _tiles = new();
    private Dictionary<Vector2, PowerNode> _powerNodes = new();

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _width = GameManager.Instance.GridWidth;
        _height = GameManager.Instance.GridHeight;
        Transform tilesParent = GameObject.Find("Tiles")?.transform;


        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                if (tilesParent != null)
                    spawnedTile.transform.SetParent(tilesParent);
                spawnedTile.name = $"Tile {x}_{y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        SpawnPowerNodes(_width, _height, GameManager.Instance.PowerNodeCount);
    }

    void SpawnPowerNodes(int width, int height, int nodeCount)
    {
        List<Vector2Int> allPositions = new();
        Transform powerNodesParent = GameObject.Find("PowerNodes")?.transform;
        

        // 1. Collect all tile positions
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            allPositions.Add(new Vector2Int(x, y));

        // 2. Shuffle the list
        for (int i = allPositions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (allPositions[i], allPositions[j]) = (allPositions[j], allPositions[i]);
        }

        // 3. Take first N positions
        for (int i = 0; i < Mathf.Min(nodeCount, allPositions.Count); i++)
        {
            Vector2Int pos = allPositions[i];
            Vector3 spawnPos = new(pos.x, pos.y, -1f);

            var node = Instantiate(_powerNodePrefab, spawnPos, Quaternion.identity);
            if (powerNodesParent != null)
                node.transform.SetParent(powerNodesParent);
            node.name = $"PowerNode {pos.x}_{pos.y}";

            _powerNodes[new Vector2(pos.x, pos.y)] = node;
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}