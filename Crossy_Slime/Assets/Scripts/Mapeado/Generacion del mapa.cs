using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public int mapWidth = 5;
    public float tileSize = 1.0f;
    public float tileLength = 1.0f;   // distancia entre filas en Z
    public float startZ = 3f;         // ¡inicio en Z = 3!
    public float startX = -1f;        // ¡primera columna en X = -1!
    public GameObject player;

    private List<GameObject[]> activeRows = new List<GameObject[]>();
    private float lastSpawnZ;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("No se encontró un jugador con la etiqueta 'Player'");
                return;
            }
        }

        // Generar filas iniciales desde startZ
        int initialRows = 5;
        for (int i = 0; i < initialRows; i++)
        {
            SpawnRow(startZ + i * tileLength);
        }
        lastSpawnZ = startZ + (initialRows - 1) * tileLength;
    }

    void Update()
    {
        if (player != null)
        {
            CheckAndSpawnNewRow(player.transform.position.z);
        }
    }

    public void CheckAndSpawnNewRow(float playerZ)
    {
        if (playerZ + tileLength * 1.5f > lastSpawnZ)
        {
            lastSpawnZ += tileLength;
            SpawnRow(lastSpawnZ);

            // Mantener solo las últimas N filas (ej: 10)
            if (activeRows.Count > 10)
            {
                GameObject[] oldRow = activeRows[0];
                activeRows.RemoveAt(0);
                foreach (GameObject tile in oldRow)
                {
                    if (tile != null) Destroy(tile);
                }
            }
        }
    }

    private void SpawnRow(float zPosition)
    {
        GameObject[] newRow = new GameObject[mapWidth];
        for (int x = 0; x < mapWidth; x++)
        {
            if (Random.value < 0.2f) // 20% vacío
            {
                newRow[x] = null;
                continue;
            }

            int randomIndex = Random.Range(0, tilePrefabs.Length);
            GameObject prefab = tilePrefabs[randomIndex];

            Vector3 position = new Vector3(
                startX + x * tileSize, // X comienza en -1, luego -1+1=0, 1, 2, ...
                0,
                zPosition
            );

            GameObject tile = Instantiate(prefab, position, Quaternion.identity);
            tile.name = $"Tile_X{x}_Z{(int)zPosition}";
            newRow[x] = tile;
        }
        activeRows.Add(newRow);
    }
}