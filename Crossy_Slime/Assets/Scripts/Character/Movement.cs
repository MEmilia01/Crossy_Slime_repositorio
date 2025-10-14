using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    
    //Son variables para poder utilizar las funciones de los detectores del script "DetectorOfGround"
    [SerializeField] DetectorOfGround detectorOfGroundForward;
    [SerializeField] DetectorOfGround detectorOfGroundBackward;
    [SerializeField] DetectorOfGround detectorOfGroundRight;
    [SerializeField] DetectorOfGround detectorOfGroundLeft;

    //Son variables para permitir moverte a un bloque dependiendo de donde te muevas

    Transform canMoveForward = null;
    Transform canMoveBackward = null;
    Transform canMoveRight = null;
    Transform canMoveLeft = null;

    Ice ice;
    private void Start()
    {
        ice = FindFirstObjectByType<Ice>();
    }

    void Update()
    {
        //Sirve para que comprobar si el personaje puede moverse en esa dirección
        //Después se almacena el pivote que se ha encontrado
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
        //Dependiendo de a que tecla le de se moverá hacia el pivote más cercano
        if (Input.GetKeyDown(KeyCode.W) && canMoveForward != null || canMoveForward != null && ice.isPlayerOnIce)
        {
            transform.position = new Vector3(canMoveForward.position.x,this.transform.position.y,canMoveForward.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveBackward != null)
        {
            transform.position = new Vector3(canMoveBackward.position.x, this.transform.position.y, canMoveBackward.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMoveRight != null)
        {
            transform.position = new Vector3(canMoveRight.position.x, this.transform.position.y, canMoveRight.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.A) && canMoveLeft != null)
        {
            transform.position = new Vector3(canMoveLeft.position.x, this.transform.position.y, canMoveLeft.position.z);
        }
    }

}
