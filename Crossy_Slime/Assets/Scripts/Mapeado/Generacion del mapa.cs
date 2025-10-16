using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GeneracionDelMapa : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab del tile
    public int mapWidth = 10; // Ancho del mapa en tiles
    public int mapHeight = 10; // Alto del mapa en tiles
    public float tileSize = 1.0f; // Tamaño de cada tile
    private GameObject[,] tiles; // Matriz para almacenar los tiles generados
    void Start()
    {
        GenerateMap();
    }
    void GenerateMap()
    {
        tiles = new GameObject[mapWidth, mapHeight];
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.name = $"Tile_{x}_{y}";
                tiles[x, y] = tile;
            }
        }
    }
}