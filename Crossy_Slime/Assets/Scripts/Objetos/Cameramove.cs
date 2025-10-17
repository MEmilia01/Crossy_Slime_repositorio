using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        float inputY = Input.GetAxis("Vertical");   // Movimiento arriba/abajo en eje Y
        float inputZ = Input.GetAxis("Horizontal"); // Movimiento adelante/atrás en eje Z

        Vector3 movimiento = new Vector3(0, inputY, inputZ) * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
    }
}
