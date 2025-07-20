using System;
using System.Collections.Generic;
using Types;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    private int _width;
    private int _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private PowerNode _powerNodePrefab;

    private Dictionary<Vector2, Tile> _tiles = new();
    private Dictionary<Vector2, PowerNode> _powerNodes = new();

    [Header("Environment")]
    [SerializeField] private GameObject[] environmentTiles;
    [SerializeField] private int environmentPadding = 4;

    private void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        DisplayRobotMovement();
    }

    private void DisplayRobotMovement()
    {
        if (GameManager.Instance.CurrentPhase != GamePhase.Plan) return;
        foreach (var kv in _tiles)
        {
            var tileGO = kv.Value.gameObject;
            var movementGO = tileGO.transform.Find("Movement")?.gameObject;
            if (movementGO != null && movementGO.activeSelf)
                movementGO.SetActive(false);
        }
        if (RobotSelectionManager.Instance.SelectedRobot == RobotType.None) return;
        if (!GameManager.Instance.HoveredTilePosition.HasValue) return;

        Vector2 hovered = GameManager.Instance.HoveredTilePosition.Value;

        int    hx       = Mathf.FloorToInt(hovered.x);
        int    hy       = Mathf.FloorToInt(hovered.y);

        if (RobotSelectionManager.Instance.SelectedRobot == RobotType.Solar)
        {
            if (RobotSelectionManager.Instance.RobotOrientation == "horizontal")
            {
                float y = hovered.y;
                for (int x = 0; x < _width; x++)
                {
                    Vector2 key = new Vector2(x, y);
                    if (_tiles.TryGetValue(key, out Tile tile))
                    {
                        var movementGO = tile.transform.Find("Movement")?.gameObject;
                        if (movementGO != null)
                            movementGO.SetActive(true);
                    }
                }
            }
            if (RobotSelectionManager.Instance.RobotOrientation == "vertical")
            {
                float x = hovered.x;
                for (int y = 0; y < _height; y++)
                {
                    Vector2 key = new Vector2(x, y);
                    if (_tiles.TryGetValue(key, out Tile tile))
                    {
                        var movementGO = tile.transform.Find("Movement")?.gameObject;
                        if (movementGO != null)
                            movementGO.SetActive(true);
                    }
                }
            }
        }
        if (RobotSelectionManager.Instance.SelectedRobot == RobotType.Coal)
        {
            // Starting at the hovered tile
            Vector2Int origin = new Vector2Int(hx, hy);

            // Directions in the new desired order: right, up, left, down
            Vector2Int[] dirs = new[]
            {
                Vector2Int.right,
                Vector2Int.up,
                Vector2Int.left,
                Vector2Int.down
            };

            int sideLength = 3; // number of steps per side

            // Highlight the origin
            if (_tiles.TryGetValue(origin, out Tile originTile))
                originTile.transform.Find("Movement")?.gameObject.SetActive(true);

            // Walk the perimeter
            Vector2Int current = origin;
            foreach (var dir in dirs)
            {
                for (int step = 1; step <= sideLength; step++)
                {
                    current += dir;
                    if (_tiles.TryGetValue(current, out Tile t))
                        t.transform.Find("Movement")?.gameObject.SetActive(true);
                }
            }
        }

    }

    void GenerateGrid()
    {
        _width = GameManager.Instance.GridWidth;
        _height = GameManager.Instance.GridHeight;

        Transform tilesParent = GameObject.Find("Tiles")?.transform;
        
        for (int x = -environmentPadding; x < _width + environmentPadding; x++)
        {
            for (int y = -environmentPadding; y < _height + environmentPadding; y++)
            {
                // Check if we're in the gameplay grid
                bool isInMainGrid = (x >= 0 && x < _width && y >= 0 && y < _height);

                if (isInMainGrid)
                {
                    // Spawn interactive tile
                    var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    if (tilesParent != null)
                        spawnedTile.transform.SetParent(tilesParent);

                    spawnedTile.name = $"Tile {x}_{y}";

                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset, new Vector2(x, y));

                    _tiles[new Vector2(x, y)] = spawnedTile;
                }
                else
                {
                    // Set decorative environment tile
                    if (environmentTiles != null && environmentTiles.Length > 0)
                    {
                        var tile = environmentTiles[Random.Range(0, environmentTiles.Length)];
                        Instantiate(tile, new Vector3Int(x, y, 0), Quaternion.identity);
                    }
                }
            }
        }

        SpawnPowerNodes(_width, _height, GameManager.Instance.PowerNodeCount);
    }
    
    void SpawnPowerNodes(int width, int height, int nodeCount)
    {
        List<Vector2Int> allPositions = new();
        Transform powerNodesParent = GameObject.Find("PowerNodes")?.transform;

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            allPositions.Add(new Vector2Int(x, y));

        for (int i = allPositions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (allPositions[i], allPositions[j]) = (allPositions[j], allPositions[i]);
        }

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
