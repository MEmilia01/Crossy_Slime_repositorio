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
                Debug.LogError("No se encontro un jugador con la etiqueta 'Player'");
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

        // Regla: bordes siempre vacios
        newRowTypes[0] = GameObjectType.Empty;
        newRowTypes[mapWidth - 1] = GameObjectType.Empty;

        // Determinar si esta fila debe ser vacia por regla de salto grande o teletransporte
        bool forceEmptyRow = false;
        float forcedTeleportRowOffset = -1; // si es 0, debemos poner el segundo teletransporte aqui

        // Revisar las ultimas 4 filas para ver si hay efectos pendientes
        int rowsBack = Mathf.Min(activeRowTypes.Count, 4);
        for (int i = 0; i < rowsBack; i++)
        {
            int rowIndex = activeRowTypes.Count - 1 - i; // fila mas reciente primero
            float distanceFromCurrent = (activeRowTypes.Count - rowIndex); // cuantas filas atras

            // Regla: salto grande → 2 filas vacias, tercera con suelo/quebradiza/hielo en punto de caida
            if (ContainsType(activeRowTypes[rowIndex], GameObjectType.LongJump))
            {
                if (distanceFromCurrent == 1 || distanceFromCurrent == 2)
                {
                    forceEmptyRow = true;
                }
                // Nota: la fila de aterrizaje es cuando distanceFromCurrent == 3.
                // La restriccion de no poner otro LongJump en esa columna se aplica mas abajo.
            }

            // Regla: teletransporte -> 2-4 filas vacias, luego el segundo teletransporte
            if (ContainsType(activeRowTypes[rowIndex], GameObjectType.Teleport) && activeTeleports == 1)
            {
                // Asumimos que el primer teletransporte ya fue colocado
                // El segundo debe aparecer entre 2 y 4 filas despues
                if (distanceFromCurrent >= 2 && distanceFromCurrent <= 4)
                {
                    forcedTeleportRowOffset = distanceFromCurrent; // usamos esto para saber si estamos en la fila correcta
                }
            }
        }

        // CASO 1: Fila forzada como vacia (por salto grande anterior)
        if (forceEmptyRow)
        {
            // Toda la fila es vacia (excepto bordes, que ya son vacios)
            for (int x = 1; x < mapWidth - 1; x++)
            {
                newRowTypes[x] = GameObjectType.Empty;
            }
        }
        // CASO 2: Fila para colocar el segundo teletransporte
        else if (forcedTeleportRowOffset >= 2 && forcedTeleportRowOffset <= 4 && activeTeleports == 1)
        {
            // Esta fila debe contener el segundo teletransporte
            // Elegimos una X valida (no en bordes)
            int teleportX = Random.Range(1, mapWidth - 1);
            for (int x = 1; x < mapWidth - 1; x++)
            {
                newRowTypes[x] = (x == teleportX) ? GameObjectType.Teleport : GameObjectType.Empty;
            }
            activeTeleports++; // ahora son 2, no se pueden generar mas
        }
        // CASO 3: Generacion normal con todas las reglas
        else
        {
            // Limitar a un solo LongJump por fila
            bool longJumpPlacedThisRow = false;

            // === Detectar si esta fila es de aterrizaje ===
            // Si 3 filas atras hubo un LongJump, esta es la fila donde el jugador cae.
            // En esa misma columna X, NO debe generarse otro LongJump.
            int landingX = -1;
            if (activeRowTypes.Count >= 3)
            {
                GameObjectType[] threeRowsBack = activeRowTypes[activeRowTypes.Count - 3];
                for (int x = 0; x < mapWidth; x++)
                {
                    if (threeRowsBack[x] == GameObjectType.LongJump)
                    {
                        landingX = x; // Recordar la columna de aterrizaje
                        break; // Solo puede haber uno por fila (garantizado por otra regla)
                    }
                }
            }

            // Generacion normal con reglas de vecindad
            for (int x = 1; x < mapWidth - 1; x++)
            {
                // Determinar si en esta columna esta prohibido el LongJump (por ser columna de aterrizaje)
                bool longJumpForbiddenHere = (x == landingX);

                // Elegir tipo de bloque, respetando: 
                // - maximo un LongJump por fila
                // - prohibicion en columna de aterrizaje
                GameObjectType type = ChooseTileType(x, activeRowTypes, longJumpPlacedThisRow, longJumpForbiddenHere);
                newRowTypes[x] = type;

                // Registrar si ya se uso el LongJump en esta fila
                if (type == GameObjectType.LongJump)
                {
                    longJumpPlacedThisRow = true;
                }

                // Contar teletransportes generados
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

    // Elige un tipo de bloque basado en probabilidades y reglas de vecindad
    // - longJumpAlreadyUsed: indica si ya se coloco un LongJump en esta fila
    // - longJumpForbiddenHere: indica si esta columna esta prohibida para LongJump (por ser columna de aterrizaje)
    private GameObjectType ChooseTileType(int x, List<GameObjectType[]> pastRows, bool longJumpAlreadyUsed, bool longJumpForbiddenHere)
    {
        // Probabilidades base
        float r = Random.value;

        // Evitar generar teletransporte si ya hay 2
        bool canTeleport = activeTeleports < 2;

        // Decidir tipo tentativo
        GameObjectType candidate;
        if (r < 0.05f && canTeleport)
        {
            candidate = GameObjectType.Teleport;
        }
        else if (r < 0.05f && !longJumpAlreadyUsed && !longJumpForbiddenHere)
        {
            candidate = GameObjectType.LongJump;
        }
        else if (r < 0.10f)
        {
            candidate = GameObjectType.Breakable;
        }
        else if (r < 0.40f)
        {
            candidate = GameObjectType.Ice;
        }
        else
        {
            candidate = GameObjectType.Ground;
        }

        // Aplicar reglas de vecindad

        // Regla: Hielo → detras debe haber suelo
        if (candidate == GameObjectType.Ice)
        {
            // "Detras" = fila anterior, misma X
            if (pastRows.Count == 0 || pastRows[pastRows.Count - 1][x] != GameObjectType.Ground)
            {
                candidate = GameObjectType.Ground; // fallback seguro
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