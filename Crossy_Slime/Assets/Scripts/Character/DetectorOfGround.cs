using UnityEngine;

public class DetectorOfGround : MonoBehaviour
{
    //Sirve para ver de forma visual como está afectando el OverlapBox
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f,0.5f,0.5f));
    }
    public Casilla GetCasilla()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.25f, 0.25f, 0.25f), Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("IsPossibleMove"))
            {
                Casilla casilla = collider.GetComponent<Casilla>();
                if (casilla != null)
                {
                    return casilla;
                }
            }
        }
        return null;
    }
}
