using UnityEngine;

public class DetectorOfGround : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Collider detector;
    bool isPossibleMove;
    public bool PossibleMove()
    {
        if (detector.CompareTag("IsPossibleMove"))
        {
            isPossibleMove = true;
            return isPossibleMove;

        }
        else
        {
            isPossibleMove = false;
            return isPossibleMove;
        }
    }
}
