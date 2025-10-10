using UnityEngine;

public class Movement : MonoBehaviour
{
    //Son variables para poder utilizar las funciones de los detectores del script "DetectorOfGround"
    [SerializeField] DetectorOfGround detectorOfGroundForward;
    [SerializeField] DetectorOfGround detectorOfGroundBackward;
    [SerializeField] DetectorOfGround detectorOfGroundRight;
    [SerializeField] DetectorOfGround detectorOfGroundLeft;

    //Son variables para permitir moverte a un bloque dependiendo de donde te muevas

    bool canMoveForward = false;
    bool canMoveBackward = false;
    bool canMoveRight = false;
    bool canMoveLeft = false;    
    void Update()
    {
        //Sirve para que comprobar si el personaje puede moverse en esa dirección
        if (detectorOfGroundForward != null)
        {
            canMoveForward = detectorOfGroundForward.PossibleMove();
        }
        if (detectorOfGroundBackward != null) 
        {
            canMoveBackward = detectorOfGroundBackward.PossibleMove();
        }
        if (detectorOfGroundRight != null)
        {
            canMoveRight = detectorOfGroundRight.PossibleMove();
        }
        if (detectorOfGroundLeft != null)
        {
            canMoveLeft = detectorOfGroundLeft.PossibleMove();
        }
        //Sirve para detectar el input del jugador para que se mueva mediante WASD dependiendo de la direccion en la que se quiera mover
        if (Input.GetKey(KeyCode.W) && canMoveForward)
        {
            transform.position += this.transform.forward * 5 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S) && canMoveBackward)
        {
            transform.position += -this.transform.forward * 5 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
            transform.position += this.transform.right * 5 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A) && canMoveLeft)
        {
            transform.position += -this.transform.right * 5 * Time.deltaTime;
        }
    }
    

}
