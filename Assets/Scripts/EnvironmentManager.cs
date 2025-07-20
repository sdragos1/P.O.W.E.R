using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Tilemap environmentTilemap;
    [SerializeField] private TileBase[] environmentTiles;
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 10;

    private void Start()
    {
    }

    private void GenerateEnvironment()
    {
        for (int x = -1; x <= width; x++)
        {
            for (int y = -1; y <= height; y++)
            {
                var tile = environmentTiles[Random.Range(0, environmentTiles.Length)];
                environmentTilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}
