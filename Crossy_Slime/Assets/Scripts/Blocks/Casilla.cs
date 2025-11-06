using UnityEngine;

public enum TipoCasillas
{
    normal, ice, teleport, longjump, breakable, dead
}

public class Casilla : MonoBehaviour
{
    public Transform Pivot;
    public TipoCasillas TCasilla;
    private Movement player;
    public bool isDead = false;
    public float chronometer = 0f;
    float chronometerMax = 4f;
    bool isStartingChronometer = false;
    public Casilla teleportDestination;
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
            if (teleportDestination != null && teleportDestination.gameObject.activeInHierarchy)
            {
                player.transform.position = teleportDestination.GetPivot().position;

                // Desactivar el destino para que no se pueda volver
                teleportDestination.TCasilla = TipoCasillas.dead;
                teleportDestination.isDead = true;
                teleportDestination.gameObject.SetActive(false);
            }
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
                MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
                meshRenderer.enabled = false;
                if (player.transform.position == GetPivot().position)
                {
                    Comportamiento(player);
                }
                isStartingChronometer = false;
            }
        }
    }

    public Transform GetPivot()
    {
        return Pivot;
    }

    public void SetTeleportDestination(Casilla destination)
    {
        teleportDestination = destination;
    }
}