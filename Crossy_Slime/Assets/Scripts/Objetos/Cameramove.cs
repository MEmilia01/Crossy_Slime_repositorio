using UnityEngine;
using DG.Tweening;

public class Cameramove : MonoBehaviour
{
    public Transform targetCamera;
    public Vector3 targetPosition;
    public float duration = 1.5f;
    private Vector3 startPosition;

    void MoveCameraTo(Vector3 endPosition, float duration)
    {
        targetCamera.DOMove(endPosition, duration);
    }
    void RotateCameraTo(Vector3 endRotation, float duration)
    {
        targetCamera.DORotate(endRotation, duration);
    }

    void Start()
    {
        
    }
    void Update()
    {        } 
}
