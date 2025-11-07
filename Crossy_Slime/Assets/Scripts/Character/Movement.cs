using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using DG.Tweening;

public class Movement : MonoBehaviour
{

    //Son variables para poder utilizar las funciones de los detectores del script "DetectorOfGround"
    [SerializeField] DetectorOfGround detectorOfGroundForward;
    [SerializeField] DetectorOfGround detectorOfGroundBackward;
    [SerializeField] DetectorOfGround detectorOfGroundRight;
    [SerializeField] DetectorOfGround detectorOfGroundLeft;
    bool inputActive = true;
    internal bool lastInput = false;
    [SerializeField] Transform direccionDeGiro;
    int inputHandlerType = 0;
    void Update()
    {
        if (inputActive)
        {
            inputHandlerType = 0;
            //Sirve para detectar el input del jugador para que se mueva mediante WASD dependiendo de la direccion en la que se quiera mover
            //Dependiendo de a que tecla le de se moverá hacia el pivote más cercano
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    inputHandlerType = 1;
                    MoveForward(true);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    inputHandlerType = 2;
                    MoveForward(true);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    inputHandlerType = 3;
                    MoveForward(true);
                }
                    
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    inputHandlerType = 1;
                    MoveBackward();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    inputHandlerType = 2;
                    MoveBackward();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    inputHandlerType = 1;
                    MoveRight();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inputHandlerType = 2;
                    MoveRight();
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    inputHandlerType = 1;
                    MoveLeft();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inputHandlerType = 2;
                    MoveLeft();
                }
            }
        }
    }
    public void MoveForward(bool comprober)
    {
        //Sirve para que comprobar si el personaje puede moverse en esa dirección
        //Después se almacena el pivote que se ha encontrado
        if (detectorOfGroundForward != null)
        {
            Casilla c = detectorOfGroundForward.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            direccionDeGiro.DORotate(new Vector3(0, 0, 0), 0);
            if (comprober)
            {
                c.Comportamiento(this);
            }
            inputActive = true;
            if (inputHandlerType == 1)
            {
                lastInput = Input.GetKeyDown(KeyCode.W);
            }
            else if(inputHandlerType == 2)
            {
                lastInput = Input.GetKeyDown(KeyCode.UpArrow);
            }
            else if(inputHandlerType == 3)
            {
                lastInput = Input.GetKeyDown(KeyCode.Space);
            }
        }
    }
    public void MoveBackward()
    {
        if (detectorOfGroundBackward != null)
        {
            Casilla c = detectorOfGroundBackward.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            direccionDeGiro.DORotate(new Vector3(0, 180, 0), 0);
            c.Comportamiento(this);
            inputActive = true;
            if (inputHandlerType == 1)
            {
                lastInput = Input.GetKeyDown(KeyCode.S);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = Input.GetKeyDown(KeyCode.DownArrow);
            }
            
        }

    }
    public void MoveRight()
    {
        if (detectorOfGroundRight != null)
        {
            Casilla c = detectorOfGroundRight.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            direccionDeGiro.DORotate(new Vector3(0, 90, 0), 0);
            c.Comportamiento(this);
            inputActive = true;
            if (inputHandlerType == 1)
            {
                lastInput = Input.GetKeyDown(KeyCode.D);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = Input.GetKeyDown(KeyCode.RightArrow);
            }
        }
    }
    public void MoveLeft()
    {
        if (detectorOfGroundLeft != null)
        {
            Casilla c = detectorOfGroundLeft.GetCasilla();
            inputActive = false;
            transform.position = c.GetPivot().position;
            direccionDeGiro.DORotate(new Vector3(0, 270, 0), 0);
            c.Comportamiento(this);
            inputActive = true;
            if (inputHandlerType == 1)
            {
                lastInput = Input.GetKeyDown(KeyCode.A);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = Input.GetKeyDown(KeyCode.LeftArrow);
            }
        }


        //---------------------------------------------------------------------------------
        /*
        //esto es para el movimiento de la camera
        Vector2Int moveDirection = new Vector2Int();    
        
        //para que la camara este un poco mas atras del jugador
        Vector3 cameraPosition = new(character.position.x + 2, 4, character.position.z - 3);
        //Limita a su vez la posicion de la camara para que no se salga
        //de ciertos limites asi no tiene, que generar de mas
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -1, 5);
        Camera.main.transform.position = cameraPosition;
        */
    }
    public void MoveOnLastDirection()
    {
        //Dependiendo del ultimo input registrado por el jugador se moverá en esa dirección
        if (lastInput == Input.GetKeyDown(KeyCode.W) || lastInput == Input.GetKeyDown(KeyCode.UpArrow) || lastInput == Input.GetKeyDown(KeyCode.Space))
        {
            MoveForward(true);
        }
        else if (lastInput == Input.GetKeyDown(KeyCode.S) || lastInput == Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBackward();
        }
        else if (lastInput == Input.GetKeyDown(KeyCode.D) || lastInput == Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (lastInput == Input.GetKeyDown(KeyCode.A) || lastInput == Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dragon"))
        {
            this.enabled = false;
        }
    }
}
