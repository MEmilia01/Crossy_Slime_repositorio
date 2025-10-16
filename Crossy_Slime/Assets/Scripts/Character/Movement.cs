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
    bool inputActive = true;
    public bool lastInput = false;
    void Update()
    {
        if (inputActive)
        {
            //Sirve para detectar el input del jugador para que se mueva mediante WASD dependiendo de la direccion en la que se quiera mover
            //Dependiendo de a que tecla le de se moverá hacia el pivote más cercano
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveForward();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveBackward();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
        }
    }
    public void MoveForward()
    {
        //Sirve para que comprobar si el personaje puede moverse en esa dirección
        //Después se almacena el pivote que se ha encontrado
        if (detectorOfGroundForward != null)
        {
            Casilla c = detectorOfGroundForward.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            c.Comportamiento();
            inputActive = true;
            lastInput = Input.GetKeyDown(KeyCode.W);
        }
    }
    public void MoveBackward()
    {
        if (detectorOfGroundBackward != null)
        {
            Casilla c = detectorOfGroundBackward.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            c.Comportamiento();
            inputActive = true;
            lastInput = Input.GetKeyDown(KeyCode.S);
        }

    }
    public void MoveRight()
    {
        if (detectorOfGroundRight != null)
        {
            Casilla c = detectorOfGroundRight.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            c.Comportamiento();
            inputActive = true;
            lastInput = Input.GetKeyDown(KeyCode.D);
        }
    }
    public void MoveLeft()
    {
        if (detectorOfGroundLeft != null)
        {
            Casilla c = detectorOfGroundLeft.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            c.Comportamiento();
            inputActive = true;
            lastInput = Input.GetKeyDown(KeyCode.A);
        }
    }

}
