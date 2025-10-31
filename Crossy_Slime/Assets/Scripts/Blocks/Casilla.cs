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
    public void Comportamiento(Movement p)
    {
        player = p;
        if (TCasilla == TipoCasillas.ice)
        {
            player.MoveOnLastDirection();

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
                Comportamiento(player);
                MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
                meshRenderer.enabled = false;
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
