using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        if (TCasilla == TipoCasillas.normal)
        {
            AudioManager.Instance.SoundGrass();
        }
        else if (TCasilla == TipoCasillas.ice)
        {
            AudioManager.Instance.SoundIce();
            if (player.lastInput == "W" || player.lastInput == "Flecha arriba" || player.lastInput == "Space")
            {
                Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0, 1), new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity);
                foreach (Collider collider in colliders)
                {
                    Casilla casilla = collider.GetComponent<Casilla>();
                    if (casilla != null)
                    {
                        player.SlideOnIce(casilla.GetPivot().position, casilla);
                    }
                }
            }
            else if (player.lastInput == "S" || player.lastInput == "Flecha abajo")
            {
                Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0, -1), new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity);
                foreach (Collider collider in colliders)
                {
                    Casilla casilla = collider.GetComponent<Casilla>();
                    if (casilla != null)
                    {
                        player.SlideOnIce(casilla.GetPivot().position, casilla);
                    }
                }
            }
            else if (player.lastInput == "D" || player.lastInput == "Flecha derecha")
            {
                Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(1, 0, 0), new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity);
                foreach (Collider collider in colliders)
                {
                    Casilla casilla = collider.GetComponent<Casilla>();
                    if (casilla != null)
                    {
                        player.SlideOnIce(casilla.GetPivot().position, casilla);
                    }
                }
            }
            else if (player.lastInput == "A" || player.lastInput == "Flecha izquierda")
            {
                Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(-1, 0, 0), new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity);
                foreach (Collider collider in colliders)
                {
                    Casilla casilla = collider.GetComponent<Casilla>();
                    if (casilla != null)
                    {
                        player.SlideOnIce(casilla.GetPivot().position, casilla);
                    }
                }
            }
        }
        else if (TCasilla == TipoCasillas.longjump)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(0, 0, 1) * 3, new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity);
            foreach (Collider collider in colliders)
            {
                Casilla casilla = collider.GetComponent<Casilla>();
                if (casilla != null)
                {
                    player.LongJump(casilla.GetPivot().position);
                }
            }
        }
        else if (TCasilla == TipoCasillas.teleport)
        {

            if (teleportDestination != null && teleportDestination.gameObject.activeInHierarchy)
            {
                if (isTeleportActivated)
                {
                    AudioManager.Instance.SoundTeleport();
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
            AudioManager.Instance.DieForVacio();
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