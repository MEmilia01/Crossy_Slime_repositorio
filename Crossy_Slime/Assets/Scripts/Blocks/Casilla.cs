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
    public float chronometer = 0f;
    float chronometerMax = 4f;
    bool isStartingChronometer = false;
    [SerializeField] internal bool isTeleportActivated = false;
    public Casilla teleportDestination;
    [SerializeField] internal Mesh teleportDeactivated;
    [SerializeField] internal MeshFilter teleportMeshDeactivated;
    public void Comportamiento(Movement p)
    {
        player = p;
        if (TCasilla == TipoCasillas.ice)
        {
            player.MoveOnLastDirection();
        }
        else if (TCasilla == TipoCasillas.longjump)
        {
            for (int i = 0; i < 2; i++)
            {
                player.MoveForward(false);

            }
            player.MoveForward(true);
        }
        else if (TCasilla == TipoCasillas.teleport)
        {

            if (teleportDestination != null && teleportDestination.gameObject.activeInHierarchy)
            {
                if (isTeleportActivated)
                {
                    player.transform.position = teleportDestination.GetPivot().position;
                    isTeleportActivated = false;
                    teleportMeshDeactivated.mesh = teleportDeactivated;
                    CloseTeleport();
                }

                // Desactivar el destino para que no se pueda volver

            }
        }
        else if (TCasilla == TipoCasillas.dead)
        {
            player.enabled = false;
        }
        else if (TCasilla == TipoCasillas.breakable)
        {
            isStartingChronometer = true;
        }
    }
    void CloseTeleport()
    {
        teleportDestination.teleportMeshDeactivated.mesh = teleportDeactivated;
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
        isTeleportActivated = true;
    }
}