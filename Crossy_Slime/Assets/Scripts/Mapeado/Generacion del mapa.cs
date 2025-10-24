using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject empty;
    public GameObject breakable;
    public GameObject ice;
    public GameObject longJump;
    public GameObject teleport;

    public int mapWidth;
    public float tileSize = 1.0f;
    public float tileLength = 1.0f;   // distancia entre filas en Z
    public float startZ = 3f;         // ¡inicio en Z = 3!
    public float startX = -2f;        // ¡primera columna en X = -1!
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
        if (playerZ + tileLength * 10f > lastSpawnZ)
        {
            lastSpawnZ += tileLength;
            SpawnRow(lastSpawnZ);

            // Mantener solo las últimas N filas (ej: 20)
            if (activeRows.Count > 20)
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
            GameObject prefab;

            // Forzar los bordes izquierdo y derecho a ser empty
            if (x == 0 || x == mapWidth - 1)
            {
                prefab = empty;
            }
            else
            {
                // Lógica original para los bloques internos
                int randomIndex = Random.Range(0, tilePrefabs.Length);
                prefab = tilePrefabs[randomIndex];

                if (Random.value < 0.1f) // 20% vacío
                {
                    prefab = empty;
                }
                else if (Random.value < 0.1f) // 10% breakable (nota: esto nunca se ejecuta si ya fue empty)
                {
                    prefab = breakable;
                }
            }

            Vector3 position = new Vector3(
                startX + x * tileSize,
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