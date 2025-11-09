using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int adelante;
    public int atras;
    public int puntuacionMax;

    [SerializeField] bool sumaAdelantePosible;
    Movement deadState;

    void Awake()
    {
        adelante = 0;
        atras = 0;
        sumaAdelantePosible = true;
    }

    void Update()
    {
        counterUpdate();
    }

    public void counterUpdate()
    {
        {
            if (sumaAdelantePosible && atras == 0)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
                {
                    adelante++;
                }
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                sumaAdelantePosible = false;
                atras++;
            } 
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                if (atras > 0)
                {
                    atras--;
                    if (atras == 0)
                    {
                        sumaAdelantePosible = true;
                    }
                }
            }

            
        }
    }
    public void GetStateOfDead(Movement dead)
    {
        deadState = dead;
        if (deadState.isDead)
        {
            if (adelante > puntuacionMax)
            {
                puntuacionMax = adelante;
                PlayerPrefs.SetInt("HighScore", puntuacionMax);
                PlayerPrefs.Save();
                Debug.Log("¡Nuevo récord guardado: " + puntuacionMax + "!");
            }
            ResetGame();
        }
    }
    public void ResetGame()
    {
        adelante = 0;
        atras = 0;
        sumaAdelantePosible = true;
        deadState.isDead = false;
    }

    // Métodos de acceso (para UI)
    public int GetCurrentScore() => adelante;
    public int GetHighScore() => puntuacionMax;
}
