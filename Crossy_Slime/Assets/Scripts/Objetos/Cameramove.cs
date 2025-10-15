using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public float velocidadVertical = 5f;
    public float velocidadMovimiento = 10f;
    public float suavizado = 0.1f;

    // Componente Rigidbody para mover el objeto con física
    public Rigidbody rb;

    // Variable para guardar la velocidad actual
    private Vector3 velocidadActual;

    void Start()
    {
        // Obtener el componente Rigidbody al inicio
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener la entrada del usuario
        float movimientoY = Input.GetAxis("Vertical");

        Debug.Log(movimientoY);
        // Calcular la velocidad deseada
        Vector3 velocidadDeseada = new Vector3(0, 0, movimientoY * velocidadVertical);

        // Aplicar suavizado para una transición más suave
        velocidadActual = Vector3.Lerp(velocidadActual, velocidadDeseada, Time.deltaTime * suavizado);

        // Aplicar la velocidad al Rigidbody
        rb.linearVelocity = new Vector3(velocidadActual.x, velocidadActual.y, rb.linearVelocity.z);
        //rb.velocity
    }
}
