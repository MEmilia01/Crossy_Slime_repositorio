using UnityEngine;

public class Cameramove : MonoBehaviour
{
   public class MovimientoDiagonal : MonoBehaviour
   {
       public float velocidad = 5f;

       void Update()
       {
           // Mover hacia arriba y hacia adelante (eje Z)
           transform.position += (Vector3.up + Vector3.forward).normalized * velocidad * Time.deltaTime;
       }
   }

   /*
    public float velocidadEje = 5f;
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
        float movimientoX = Input.GetAxis("Horizontal");

        //Debug.Log(movimientoX);
        // Calcular la velocidad deseada
        Vector3 velocidadDeseada = new Vector3(movimientoX * velocidadEje, 0, 0);

        // Aplicar suavizado para una transición más suave
        velocidadActual = Vector3.Lerp(velocidadActual, velocidadDeseada, Time.deltaTime * suavizado);

        // Aplicar la velocidad al Rigidbody
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, velocidadActual.y, velocidadActual.z);
        //rb.velocity
    }
    */
}
