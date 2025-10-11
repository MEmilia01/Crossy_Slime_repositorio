using UnityEngine;

public class DetectorOfGround : MonoBehaviour
{
    //Transform para almacenar el pivote al que queremos movernos
    Transform PossibleMovement = null;
    //Sirve para ver de forma visual como está afectando el OverlapBox
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
    public Transform PossibleMove()
    {
        //Se utiliza para detectar las colisiones que están sobreponiendose al detector
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        foreach (Collider collider in colliders)
        {
            //Por cada colisión que tenga la tag de "IsPossibleMove"
            if (collider.CompareTag("IsPossibleMove"))
            {
                //Recibira el componente de Pick_The_Pivot y se guardara
                Pick_The_Pivot isPossibleMove = collider.GetComponent<Pick_The_Pivot>();
                PossibleMovement = isPossibleMove.GetPivot();
            }
        }
        //Se envia el el pivote
        return PossibleMovement;
    }
}
