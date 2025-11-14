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
    public GameObject dragon;

    public int mapWidth = 13;
    public float tileSize = 1.0f;
    public float tileLength = 1.0f;
    public float startZ = 3f;
    public float startX = -2f;
    public GameObject player;

    private List<GameObject[]> activeRows = new List<GameObject[]>();
    private List<GameObjectType[]> activeRowTypes = new List<GameObjectType[]>();
    private float lastSpawnZ;
    private int activeTeleports = 0; // 0 = ninguno, 1 = origen colocado, 2 = par completo
    private bool teleportPairIsActive = false; // true mientras origen o destino esten en activeRowTypes

    // Referencia al GameObject del origen (para vincular despues)
    private GameObject teleportOriginObject = null;
    private int teleportOriginX = -1;

    private enum GameObjectType
    {
        Empty,
        Ground,
        Breakable,
        Ice,
        LongJump,
        Teleport,
        Dragon
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
                GameObjectType[] oldRowTypes = activeRowTypes[0];

                // Verificar si la fila que vamos a destruir contiene parte del par de teletransporte
                bool containedTeleport = ContainsType(oldRowTypes, GameObjectType.Teleport);

                activeRows.RemoveAt(0);
                activeRowTypes.RemoveAt(0);

                foreach (GameObject tile in oldRow)
                {
                    if (tile != null) Destroy(tile);
                }

                // Si el par estaba activo y acabamos de destruir una fila con teletransporte,
                // revisar si ya NO quedan teletransportes en las filas activas
                if (teleportPairIsActive && containedTeleport)
                {
                    bool anyTeleportRemains = false;
                    foreach (var row in activeRowTypes)
                    {
                        if (ContainsType(row, GameObjectType.Teleport))
                        {
                            anyTeleportRemains = true;
                            break;
                        }
                    }

                    if (!anyTeleportRemains)
                    {
                        // Ya no hay teletransportes en pantalla: permitir un nuevo par
                        activeTeleports = 0;
                        teleportPairIsActive = false;
                        teleportOriginObject = null;
                        teleportOriginX = -1;
                    }
                }
            }
        }
    }

    private void SpawnRow(float zPosition)
    {
        GameObjectType[] newRowTypes = new GameObjectType[mapWidth];
        newRowTypes[0] = GameObjectType.Empty;
        newRowTypes[mapWidth - 1] = GameObjectType.Empty;

        bool forceEmptyRow = false;
        bool isTeleportLandingRow = false;

        // Revisar efectos pendientes
        int rowsBack = Mathf.Min(activeRowTypes.Count, 5);
        for (int i = 0; i < rowsBack; i++)
        {
            int rowIndex = activeRowTypes.Count - 1 - i;
            int distanceFromCurrent = activeRowTypes.Count - rowIndex;

            if (ContainsType(activeRowTypes[rowIndex], GameObjectType.LongJump))
            {
                if (distanceFromCurrent == 1 || distanceFromCurrent == 2)
                {
                    forceEmptyRow = true;
                }
            }

            // Teletransporte: 3 filas vacias, destino en la cuarta
            if (ContainsType(activeRowTypes[rowIndex], GameObjectType.Teleport) && activeTeleports == 1)
            {
                if (distanceFromCurrent >= 1 && distanceFromCurrent <= 3)
                {
                    forceEmptyRow = true;
                }
                else if (distanceFromCurrent == 4)
                {
                    isTeleportLandingRow = true;
                }
            }
        }

        if (forceEmptyRow)
        {
            for (int x = 1; x < mapWidth - 1; x++)
            {
                newRowTypes[x] = GameObjectType.Empty;
            }
        }
        else if (isTeleportLandingRow && activeTeleports == 1)
        {
            // === Fila del destino: todo GROUND excepto UN bloque de Teleport (en X != origen) ===
            for (int x = 1; x < mapWidth - 1; x++)
            {
                newRowTypes[x] = GameObjectType.Ground;
            }

            // Elegir una X valida para el destino, DISTINTA de la del origen
            int destinationX = -1;
            if (teleportOriginX >= 1 && teleportOriginX <= mapWidth - 2)
            {
                // Crear lista de columnas validas (1 a mapWidth-2) excluyendo teleportOriginX
                List<int> validColumns = new List<int>();
                for (int x = 1; x < mapWidth - 1; x++)
                {
                    if (x != teleportOriginX)
                    {
                        validColumns.Add(x);
                    }
                }

                if (validColumns.Count > 0)
                {
                    destinationX = validColumns[Random.Range(0, validColumns.Count)];
                }
                else
                {
                    // Caso extremo (mapWidth = 3), forzar offset
                    destinationX = (teleportOriginX + 1) % mapWidth;
                    if (destinationX == 0 || destinationX == mapWidth - 1) destinationX = 1;
                }
            }
            else
            {
                // Fallback: elegir cualquier X valida
                destinationX = Random.Range(1, mapWidth - 1);
            }

            newRowTypes[destinationX] = GameObjectType.Teleport;
            activeTeleports = 2; // Par completo
        }
        else
        {
            // Generacion normal
            bool longJumpPlacedThisRow = false;
            bool teleportPlacedThisRow = false;

            int landingX = -1;
            if (activeRowTypes.Count >= 3)
            {
                var threeRowsBack = activeRowTypes[activeRowTypes.Count - 3];
                for (int x = 0; x < mapWidth; x++)
                {
                    if (threeRowsBack[x] == GameObjectType.LongJump)
                    {
                        landingX = x;
                        break;
                    }
                }
            }

            for (int x = 1; x < mapWidth - 1; x++)
            {
                // si esta es la columna de aterrizaje, forzar Ground
                if (x == landingX)
                {
                    newRowTypes[x] = GameObjectType.Ground;
                    continue; // Saltar elección aleatoria
                }

                bool longJumpForbiddenHere = false; 
                bool canPlaceTeleport = (activeTeleports == 0);

                GameObjectType type = ChooseTileType(
                    x,
                    activeRowTypes,
                    longJumpPlacedThisRow,
                    longJumpForbiddenHere,
                    canPlaceTeleport,
                    teleportPlacedThisRow
                );

                newRowTypes[x] = type;

                if (type == GameObjectType.LongJump)
                {
                    longJumpPlacedThisRow = true;
                }
                if (type == GameObjectType.Teleport)
                {
                    teleportPlacedThisRow = true;
                    activeTeleports = 1;
                    teleportOriginX = x;
                    teleportPairIsActive = true;
                }
            }
        }

        // === Instanciar y guardar referencias ===
        //aca es lo de la puntuacion ^^
        GameObject[] newRow = new GameObject[mapWidth];
        for (int x = 0; x < mapWidth; x++)
        {
            GameObject prefab = GetPrefabFromType(newRowTypes[x]);
            Vector3 position = new Vector3(startX + x * tileSize, 0, zPosition);
            GameObject tile = Instantiate(prefab, position, Quaternion.identity);
            tile.name = $"Tile_X{x}_Z{(int)zPosition}";
            newRow[x] = tile;

            // Guardar referencia al origen
            if (newRowTypes[x] == GameObjectType.Teleport && activeTeleports == 1 && !isTeleportLandingRow)
            {
                teleportOriginObject = tile;
            }
        }

        activeRows.Add(newRow);
        activeRowTypes.Add(newRowTypes);

        // === Vincular origen y destino si acabamos de generar el destino ===
        if (isTeleportLandingRow && activeTeleports == 2 && teleportOriginObject != null)
        {
            // Buscar el GameObject del destino en la fila recien creada
            GameObject teleportDestinationObject = null;
            for (int x = 0; x < mapWidth; x++)
            {
                if (newRow[x] != null && newRowTypes[x] == GameObjectType.Teleport)
                {
                    teleportDestinationObject = newRow[x];
                    break;
                }
            }

            if (teleportDestinationObject != null)
            {
                // Obtener los componentes Casilla
                Casilla originCasilla = teleportOriginObject.GetComponent<Casilla>();
                Casilla destCasilla = teleportDestinationObject.GetComponent<Casilla>();

                if (originCasilla != null && destCasilla != null)
                {
                    originCasilla.SetTeleportDestination(destCasilla);
                    // El destino no necesita apuntar a ningun lado
                }
            }
        }
    }

    private GameObjectType ChooseTileType(
        int x,
        List<GameObjectType[]> pastRows,
        bool longJumpAlreadyUsed,
        bool longJumpForbiddenHere,
        bool canPlaceTeleport,
        bool teleportAlreadyPlaced)
    {
        float r = Random.value;

        // Importante: si hay LongJump en la fila, no permitir teletransporte
        bool allowTeleport = canPlaceTeleport && !longJumpAlreadyUsed && !teleportAlreadyPlaced;
        // Importante: si hay teletransporte en la fila, no permitir LongJump
        bool allowLongJump = !longJumpAlreadyUsed && !longJumpForbiddenHere && !teleportAlreadyPlaced;

        GameObjectType candidate;
        if (r < 0.03f && allowTeleport) // Reducida probabilidad para equilibrio
        {
            candidate = GameObjectType.Teleport;
        }
        else if (r < 0.05f && allowLongJump)
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

        // Regla del hielo
        if (candidate == GameObjectType.Ice)
        {
            if (pastRows.Count == 0 || pastRows[pastRows.Count - 1][x] == GameObjectType.Ground || pastRows[pastRows.Count - 1][x] == GameObjectType.Ice)
            {
                candidate = GameObjectType.Ice;
            }
        }

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
            case GameObjectType.Dragon: return dragon;
            default: return empty;
        }
    }
}