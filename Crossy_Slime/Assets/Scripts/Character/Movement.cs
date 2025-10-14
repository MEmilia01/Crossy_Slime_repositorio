using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

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
        if (Input.GetKeyDown(KeyCode.W) && canMoveForward != null)
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

        //esto es para el movimiento de la camera
        /*
        if (moveDirection != Vector2Int.zero)
        {
            Vector2Int destination = characterPos + moveDirection;
            //in the start area there are no obstacle so you can move anywhere 
            if (InStartArea(destination) || ((destination.y >= 0) && !obstacles[destination.y].locations.Contains(destination.x)))
            {//update our character grid coordinate
                characterPos == destination;
                //call coroutine to meve the character objetc
                StartCoroutine(MoveCharacter());
            }
        }
        //para que la camara este un poco mas atras del jugador
        Vector3 cameraPosition = new(character.position.x + 2, 4, character.position.z - 3);
        
        
        //Limita a su vez la posicion de la camara para que no se salga
        //de ciertos limites asi no tiene, que generar de mas
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -1, 5);
        Camera.main.transform.position = cameraPosition;
        */
    }
}
