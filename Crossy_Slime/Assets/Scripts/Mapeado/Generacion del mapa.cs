using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    public GameObject empty;
    public GameObject breakable;
    public GameObject ice;
    public GameObject longJump;
    public GameObject teleport;
    public GameObject ground;

    public int mapWidth = 13; 
    public float tileSize = 1.0f;
    public float tileLength = 1.0f;
    public float startZ = 3f;
    public float startX = -2f;
    public GameObject player;

    private List<GameObject[]> activeRows = new List<GameObject[]>();
    private List<GameObjectType[]> activeRowTypes = new List<GameObjectType[]>(); // para logica sin instanciar
    private float lastSpawnZ;
    private int activeTeleports = 0; // maximo 2

    private enum GameObjectType
    {
        Empty,
        Ground,
        Breakable,
        Ice,
        LongJump,
        Teleport
    }

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

            if (activeRows.Count > 20)
            {
                GameObject[] oldRow = activeRows[0];
                activeRows.RemoveAt(0);
                activeRowTypes.RemoveAt(0);
                foreach (GameObject tile in oldRow)
                {
                    if (tile != null) Destroy(tile);
                }
            }
        }
    }

    private void SpawnRow(float zPosition)
    {
        // Crear fila de tipos primero (logica sin instanciar)
        GameObjectType[] newRowTypes = new GameObjectType[mapWidth];

        // Regla: bordes siempre vacíos
        newRowTypes[0] = GameObjectType.Empty;
        newRowTypes[mapWidth - 1] = GameObjectType.Empty;

        // Determinar si esta fila debe ser vacía por regla de salto grande o teletransporte
        bool forceEmptyRow = false;
        float forcedTeleportRowOffset = -1; // si es 0, debemos poner el segundo teletransporte aqui

        // Revisar las últimas 4 filas para ver si hay efectos pendientes
        int rowsBack = Mathf.Min(activeRowTypes.Count, 4);
        for (int i = 0; i < rowsBack; i++)
        {
            int rowIndex = activeRowTypes.Count - 1 - i; // fila mas reciente primero
            float distanceFromCurrent = (activeRowTypes.Count - rowIndex); // cuantas filas atras

            // Regla: salto grande → 2 filas vacías, tercera con suelo/quebradiza/hielo en punto de caida
            if (ContainsType(activeRowTypes[rowIndex], GameObjectType.LongJump))
            {
                if (distanceFromCurrent == 1 || distanceFromCurrent == 2)
                {
                    forceEmptyRow = true;
                }
                else if (distanceFromCurrent == 3)
                {
                    // En esta fila, en la misma columna X del salto, debe haber suelo/quebradiza/hielo
                    // Por simplicidad, permitimos generar normalmente, pero evitamos vacío en esa X
                    // (opcional: guardar la X del salto y forzar ahí)
                }
            }

            // Regla: teletransporte -> 2-4 filas vacias, luego el segundo teletransporte
            if (ContainsType(activeRowTypes[rowIndex], GameObjectType.Teleport) && activeTeleports == 1)
            {
                // Asumimos que el primer teletransporte ya fue colocado
                // El segundo debe aparecer entre 2 y 4 filas después
                if (distanceFromCurrent >= 2 && distanceFromCurrent <= 4)
                {
                    forcedTeleportRowOffset = distanceFromCurrent; // usamos esto para saber si estamos en la fila correcta
                }
            }
        }

        if (forceEmptyRow)
        {
            // Toda la fila es vacía (excepto bordes, que ya son vacíos)
            for (int x = 1; x < mapWidth - 1; x++)
            {
                newRowTypes[x] = GameObjectType.Empty;
            }
        }
        else if (forcedTeleportRowOffset >= 2 && forcedTeleportRowOffset <= 4 && activeTeleports == 1)
        {
            // Esta fila debe contener el segundo teletransporte
            // Elegimos una X válida (no en bordes)
            int teleportX = Random.Range(1, mapWidth - 1);
            for (int x = 1; x < mapWidth - 1; x++)
            {
                newRowTypes[x] = (x == teleportX) ? GameObjectType.Teleport : GameObjectType.Empty;
            }
            activeTeleports++; // ahora son 2, no se pueden generar mas
        }
        else
        {
            // Generación normal con reglas de vecindad
            for (int x = 1; x < mapWidth - 1; x++)
            {
                GameObjectType type = ChooseTileType(x, newRowTypes, activeRowTypes);
                newRowTypes[x] = type;

                if (type == GameObjectType.Teleport && activeTeleports < 2)
                {
                    activeTeleports++;
                }
            }

            // Si generamos un teletransporte y es el primero, las proximas 2-4 filas deben ser vacias y luego el segundo
            // (eso se manejara en futuras iteraciones)
        }

        // Instanciar los GameObjects
        GameObject[] newRow = new GameObject[mapWidth];
        for (int x = 0; x < mapWidth; x++)
        {
            GameObject prefab = GetPrefabFromType(newRowTypes[x]);
            Vector3 position = new Vector3(startX + x * tileSize, 0, zPosition);
            GameObject tile = Instantiate(prefab, position, Quaternion.identity);
            tile.name = $"Tile_X{x}_Z{(int)zPosition}";
            newRow[x] = tile;
        }

        activeRows.Add(newRow);
        activeRowTypes.Add(newRowTypes);
    }

    private GameObjectType ChooseTileType(int x, GameObjectType[] currentRow, List<GameObjectType[]> pastRows)
    {
        // Probabilidades base
        float r = Random.value;

        // Evitar generar teletransporte si ya hay 2
        bool canTeleport = activeTeleports < 2;

        // Decidir tipo tentativo
        GameObjectType candidate;
        if (r < 0.05f && canTeleport)
            candidate = GameObjectType.Teleport;
        else if (r < 0.15f)
            candidate = GameObjectType.LongJump;
        else if (r < 0.30f)
            candidate = GameObjectType.Breakable;
        else if (r < 0.45f)
            candidate = GameObjectType.Ice;
        else if (r < 0.90f)
            candidate = GameObjectType.Ground;
        else
            candidate = GameObjectType.Empty;

        // Aplicar reglas de vecindad

        // Regla: Hielo → detrás debe haber suelo, delante debe haber suelo/tele/longjump/breakable
        if (candidate == GameObjectType.Ice)
        {
            // "Detras" = fila anterior, misma X
            if (pastRows.Count == 0 || pastRows[pastRows.Count - 1][x] != GameObjectType.Ground)
            {
                candidate = GameObjectType.Ground; // fallback seguro
            }
            // "Delante" = se validara al generar la siguiente fila (no podemos garantizarlo ahora)
            // Pero al menos evitamos hielo al final del mapa sin salida
            if (x == 1 || x == mapWidth - 2)
            {
                // Si esta en el borde interior, asegurar que haya salida
                bool hasExit = false;
                // Revisar izquierda/derecha/frente en futuras filas -> difícil ahora
                // Por simplicidad, evitar hielo en bordes si no hay suelo al lado
                if ((x > 1 && currentRow[x - 1] == GameObjectType.Ground) ||
                    (x < mapWidth - 2 && currentRow[x + 1] == GameObjectType.Ground))
                {
                    hasExit = true;
                }
                if (!hasExit)
                {
                    candidate = GameObjectType.Ground;
                }
            }
        }

        // Regla: Plataforma quebradiza → debe tener suelo o hielo adyacente (izq/der/frente)
        if (candidate == GameObjectType.Breakable)
        {
            bool hasAdjacentSafe = false;

            // Izquierda/derecha en misma fila (ya generadas parcialmente)
            if (x > 1 && currentRow[x - 1] != GameObjectType.Empty)
                hasAdjacentSafe = true;
            if (x < mapWidth - 2 && currentRow[x + 1] != GameObjectType.Empty)
                hasAdjacentSafe = true;

            // Frente = fila anterior, misma X
            if (pastRows.Count > 0 && pastRows[pastRows.Count - 1][x] != GameObjectType.Empty)
                hasAdjacentSafe = true;

            if (!hasAdjacentSafe)
            {
                candidate = GameObjectType.Ground;
            }
        }

        // Regla: Salto grande -> no se valida aqui, se fuerzan filas vacias despues
        // Regla: Teletransporte -> ya limitado por contador

        return candidate;
    }

    private bool ContainsType(GameObjectType[] row, GameObjectType type)
    {
        foreach (var t in row)
        {
            if (t == type) return true;
        }
        return false;
    }

    private GameObject GetPrefabFromType(GameObjectType type)
    {
        switch (type)
        {
            case GameObjectType.Empty: return empty;
            case GameObjectType.Ground: return ground;
            case GameObjectType.Breakable: return breakable;
            case GameObjectType.Ice: return ice;
            case GameObjectType.LongJump: return longJump;
            case GameObjectType.Teleport: return teleport;
            default: return empty;
        }
    }
}