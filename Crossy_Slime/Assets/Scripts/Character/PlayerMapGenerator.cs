using UnityEngine;

public class PlayerMapGenerator : MonoBehaviour
{
    public ProceduralMapGenerator mapGenerator;

    void Update()
    {
        // Suponiendo que el jugador se mueve en Z
        if (mapGenerator != null)
        {
            mapGenerator.CheckAndSpawnNewRow(transform.position.z);
        }
    }
}
