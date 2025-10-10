using UnityEngine;

public class DetectorOfGround : MonoBehaviour
{
    //Booleano para permitir si puede moverse o no
    bool isPossibleMove;

    private void OnTriggerEnter(Collider other)
    {
        //Si tiene la tag IsPossibleMove podra moverse
        if (other.CompareTag("IsPossibleMove"))
        {            
            isPossibleMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Al salir lo convierte en falso
        if (other.CompareTag("IsPossibleMove"))
        {
            isPossibleMove = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Si tiene la tag IsPossibleMove podra moverse
        if (other.CompareTag("IsPossibleMove"))
        {
            isPossibleMove = true;
        }
    }
    //Devuelve el booleano para poder mover el personaje en el script "Movement"
    public bool PossibleMove()
    {
        return isPossibleMove;
    }
}
