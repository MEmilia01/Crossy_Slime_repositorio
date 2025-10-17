using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    // Tipos de tiles (excluimos "Vacio" porque no se instancia)
    public GameObject[] tilePrefabs; // Arrastra aquí: Suelo, Hielo, Teletransporte, saltoGrande, Temporal

    public int mapWidth = 5;          // Ancho del camino (eje X)
    public float tileSize = 1.0f;     // Tamaño de cada tile
    public float spawnZ = 0f;         // Posición Z inicial
    public float tileLength = 1.0f;   // Longitud de cada fila en Z (normalmente = tileSize)

    private List<GameObject[]> activeRows = new List<GameObject[]>(); // Filas activas
    private float lastSpawnZ;

    void Start()
    {
        // Generar las primeras N filas para que el jugador tenga donde empezar
        int initialRows = 5;
        for (int i = 0; i < initialRows; i++)
        {
            SpawnRow(spawnZ + i * tileLength);
        }
        lastSpawnZ = spawnZ + (initialRows - 1) * tileLength;
    }

    // Llamar desde el jugador o desde Update para chequear si hay que generar más
    public void CheckAndSpawnNewRow(float playerZ)
    {
        // Si el jugador está a medio camino de la última fila, genera una nueva
        if (playerZ + tileLength * 1.5f > lastSpawnZ)
        {
            lastSpawnZ += tileLength;
            SpawnRow(lastSpawnZ);

            // Opcional: eliminar filas viejas para ahorrar memoria
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
            // Decidir si hay tile o no (ej: 20% de probabilidad de vacío)
            if (Random.value < 0.2f) // 20% de casillas vacías
            {
                newRow[x] = null; // No hay tile
                continue;
            }

            // Elegir un tipo de tile aleatorio
            int randomIndex = Random.Range(0, tilePrefabs.Length);
            GameObject prefab = tilePrefabs[randomIndex];

            Vector3 position = new Vector3(
                (x - mapWidth / 2.0f) * tileSize, // Centrado en X=0
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