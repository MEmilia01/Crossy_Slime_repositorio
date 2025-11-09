using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int adelante;
    public int atras;
    public int puntuacionMax;
    internal bool havePlayerMoved;
    internal bool isAvailableMove = false;

    Movement deadState;
    Movement player;

    void Awake()
    {
        adelante = 0;
        atras = 0;
    }

    void Update()
    {
        counterUpdate();
    }

    public void counterUpdate()
    {
        if (isAvailableMove)
        {
            if (havePlayerMoved)
            {
                if (atras > 0)
                {
                    atras--;
                    isAvailableMove = false;
                }
                else if (atras <= 0)
                {
                    adelante++;
                    isAvailableMove = false;
                }
            }
            if (!havePlayerMoved)
            {
                atras++;
                isAvailableMove = false;
            }
        }
        
    }
    internal void GetMovementForward(Movement p)
    {
        player = p;
        havePlayerMoved = true;
        isAvailableMove = true;
    }
    internal void GetMovementBackward(Movement p)
    {
        player = p;
        havePlayerMoved = false;
        isAvailableMove = true;
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
        deadState.isDead = false;
    }

    // Métodos de acceso (para UI)
    public int GetCurrentScore() => adelante;
    public int GetHighScore() => puntuacionMax;
}
