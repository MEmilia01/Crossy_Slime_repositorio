using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public float speed = 5f; // velocidad de la cámara
    public KeyCode activateKey = KeyCode.Space; // tecla para activar movimiento

    private bool isMoving = false; // controla si la cámara se mueve

        void Update()
        {
            // Detectar pulsación para activar/desactivar el movimiento
            if (Input.GetKeyDown(activateKey))
            {
                isMoving = !isMoving; // cambia estado de movimiento
            }

            // Si está activado, mover la cámara
            if (isMoving)
            {
                Vector3 direction = new Vector3(0, 1, 1).normalized; // dirección entre ejes Y y Z
                transform.position += direction * speed * Time.deltaTime;
            }

        } 
}
