using UnityEngine;
using UnityEngine.Rendering;
public enum TipoCasillas
{
    normal, ice, teleport, longjump, breakable, dead
}
public class Casilla : MonoBehaviour
{
    public Transform Pivot;
    public TipoCasillas TCasilla;
    Movement player;
    public bool isDead = false;
    public float chronometer = 0f;
    float chronometerMax = 4f;
    bool isStartingChronometer = false;
    private void Start()
    {
        player = FindFirstObjectByType<Movement>();
    }
    public void Comportamiento()
    {
        if (TCasilla == TipoCasillas.ice)
        {
            //Dependiendo del ultimo input registrado por el jugador se moverá en esa dirección
            if (player.lastInput == Input.GetKeyDown(KeyCode.W))
            {
                player.MoveForward(true);
            }
            else if (player.lastInput == Input.GetKeyDown(KeyCode.S))
            {
                player.MoveBackward();
            }
            else if (player.lastInput == Input.GetKeyDown(KeyCode.D))
            {
                player.MoveRight();
            }
            else if (player.lastInput == Input.GetKeyDown(KeyCode.A))
            {
                player.MoveLeft();
            }

        }
        else if (TCasilla == TipoCasillas.longjump)
        {
            for (int i = 0; i < 3; i++)
            {
                player.MoveForward(false);
            }
        }
        else if (TCasilla == TipoCasillas.teleport)
        {

        }
        else if (TCasilla == TipoCasillas.dead)
        {
            isDead = true;
            player.enabled = false;

        }
        else if (TCasilla == TipoCasillas.breakable)
        {
            isStartingChronometer = true;

        }
    }
    private void Update()
    {
        if (isStartingChronometer)
        {
            chronometer += Time.deltaTime;
            if (chronometer >= chronometerMax)
            {
                this.TCasilla = TipoCasillas.dead;
                isStartingChronometer = false;
            }
        }
    }
    //Recibimos el pivote 
    public Transform GetPivot()
    {
        return Pivot;
    }
}
