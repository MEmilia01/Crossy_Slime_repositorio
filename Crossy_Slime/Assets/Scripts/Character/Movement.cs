using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{

    //Son variables para poder utilizar las funciones de los detectores del script "DetectorOfGround"
    [SerializeField] DetectorOfGround detectorOfGroundForward;
    [SerializeField] DetectorOfGround detectorOfGroundBackward;
    [SerializeField] DetectorOfGround detectorOfGroundRight;
    [SerializeField] DetectorOfGround detectorOfGroundLeft;
    bool inputActive = true;
    internal string lastInput = " ";
    [SerializeField] Transform direccionDeGiro;
    [SerializeField] Transform agitacionMuerte;
    int inputHandlerType = 0;
    [SerializeField] Mesh slimeMuerto;
    [SerializeField] MeshFilter slimeDead;
    bool isAllowedAnimation = true;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private float mapStartZ = 3f;
    [SerializeField] private float tileLength = 1f;
    Tween jump;
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
                    MoveForward();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    inputHandlerType = 2;
                    MoveForward();
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    inputHandlerType = 3;
                    MoveForward();
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
    public void MoveForward()
    {
        //Sirve para que comprobar si el personaje puede moverse en esa dirección
        //Después se almacena el pivote que se ha encontrado
        if (detectorOfGroundForward != null)
        {
            Casilla c = detectorOfGroundForward.GetCasilla();
            if (scoreManager != null && c != null)
            {
                // Calcular índice de fila desde la posición Z del pivote (o del tile)
                float z = c.GetPivot().position.z; // o c.transform.position.z
                int rowIndex = Mathf.FloorToInt((z - mapStartZ) / tileLength);
                scoreManager.NewIndex(rowIndex);
            }
            inputActive = false;
            //transform.position = c.GetPivot().position;

            jump.Kill();
            direccionDeGiro.DORotate(new Vector3(0, 0, 0), 0);
            AudioManager.Instance.Walking();
            if (inputHandlerType == 1)
            {
                lastInput = "W";
                PlayerPrefs.SetString("W", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = "Flecha arriba";
                PlayerPrefs.SetString("Flecha arriba", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            else if (inputHandlerType == 3)
            {
                lastInput = "Space";
                PlayerPrefs.SetString("Space", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            jump = this.gameObject.transform.DOJump(c.GetPivot().position, 1, 1, 0.05f)
                .OnComplete(() =>
                {
                    inputActive = true;
                    c.Comportamiento(this);
                });
            //inputActive = true;

        }
    }
    public void MoveBackward()
    {
        if (detectorOfGroundBackward != null)
        {
            Casilla c = detectorOfGroundBackward.GetCasilla();
            if (scoreManager != null && c != null)
            {
                // Calcular índice de fila desde la posición Z del pivote (o del tile)
                float z = c.GetPivot().position.z; // o c.transform.position.z
                int rowIndex = Mathf.FloorToInt((z - mapStartZ) / tileLength);
                scoreManager.NewIndex(rowIndex);
            }
            inputActive = false;
            jump.Kill();
            direccionDeGiro.DORotate(new Vector3(0, 180, 0), 0);
            AudioManager.Instance.Walking();
            if (inputHandlerType == 1)
            {
                lastInput = "S";
                PlayerPrefs.SetString("S", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = "Flecha abajo";
                PlayerPrefs.SetString("Flecha abajo", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            jump = this.gameObject.transform.DOJump(c.GetPivot().position, 1, 1, 0.05f)
                .OnComplete(() =>
                {
                    inputActive = true;
                    c.Comportamiento(this);
                });


        }

    }
    public void MoveRight()
    {
        if (detectorOfGroundRight != null)
        {
            Casilla c = detectorOfGroundRight.GetCasilla();
            if (scoreManager != null && c != null)
            {
                // Calcular índice de fila desde la posición Z del pivote (o del tile)
                float z = c.GetPivot().position.z; // o c.transform.position.z
                int rowIndex = Mathf.FloorToInt((z - mapStartZ) / tileLength);
                scoreManager.NewIndex(rowIndex);
            }
            inputActive = false;
            jump.Kill();
            direccionDeGiro.DORotate(new Vector3(0, 90, 0), 0);
            AudioManager.Instance.Walking();
            if (inputHandlerType == 1)
            {
                lastInput = "D";
                PlayerPrefs.SetString("D", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = "Flecha derecha";
                PlayerPrefs.SetString("Flecha derecha", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            jump = this.gameObject.transform.DOJump(c.GetPivot().position, 1, 1, 0.05f)
                .OnComplete(() =>
                {
                    inputActive = true;
                    c.Comportamiento(this);
                });



        }
    }
    public void MoveLeft()
    {
        if (detectorOfGroundLeft != null)
        {
            Casilla c = detectorOfGroundLeft.GetCasilla();
            if (scoreManager != null && c != null)
            {
                // Calcular índice de fila desde la posición Z del pivote (o del tile)
                float z = c.GetPivot().position.z; // o c.transform.position.z
                int rowIndex = Mathf.FloorToInt((z - mapStartZ) / tileLength);
                scoreManager.NewIndex(rowIndex);
            }
            inputActive = false;
            jump.Kill();
            direccionDeGiro.DORotate(new Vector3(0, 270, 0), 0);
            AudioManager.Instance.Walking();
            if (inputHandlerType == 1)
            {
                lastInput = "A";
                PlayerPrefs.SetString("A", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            else if (inputHandlerType == 2)
            {
                lastInput = "Flecha izquierda";
                PlayerPrefs.SetString("Flecha izquierda", lastInput);
                PlayerPrefs.Save(); 
                Debug.Log("Input guardado: " + lastInput);
            }
            jump = this.gameObject.transform.DOJump(c.GetPivot().position, 1, 1, 0.05f)
                .OnComplete(() =>
                {
                    inputActive = true;
                    c.Comportamiento(this);
                });


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
    public void SlideOnIce(Vector3 positionCasilla, Casilla c)
    {
        inputActive = false;
        jump.Kill();

        jump = this.gameObject.transform.DOMove(positionCasilla, 0.05f)
            .OnComplete(() =>
            {
                inputActive = true;
                c.Comportamiento(this);
            });
    }
    public void LongJump(Vector3 position)
    {
        inputActive = false;
        jump.Kill();

        jump = this.gameObject.transform.DOJump(position, 1, 1, 0.1f)
            .OnComplete(() => inputActive = true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dragon"))
        {
            scoreManager?.GameCompleted();
            this.enabled = false;
            if (isAllowedAnimation)
            {
                DoAnimationOfDead();
                AudioManager.Instance.DieForDragon();
            }
        }
    }
    void DoAnimationOfDead()
    {
        agitacionMuerte.DOShakeScale(3, 1, 4, 0);
        slimeDead.mesh = slimeMuerto;
        isAllowedAnimation = false;
    }

    public void ResetPlayer()
    {
        scoreManager?.ResetScore();
        isAllowedAnimation = true;
        slimeDead.mesh = null; // o el mesh original
        this.enabled = true;
        // Reinicia posición, rotación, etc.
    }
}
