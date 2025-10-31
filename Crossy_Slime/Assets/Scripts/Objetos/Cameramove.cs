using UnityEngine;
using DG.Tweening;

public class Cameramove : MonoBehaviour
{
    public Transform targetCamera;
    public Vector3 targetPosition;
    public float duration = 1.5f;
    private Vector3 startPosition;
    private bool isTweening = false;

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
        if (targetCamera != null)
        {
            Vector3 targetPos = new Vector3(10, 5, -20);
            MoveCameraTo(targetPos, 2f); // Move camera over 2 seconds
        }
    }

    public void StartTween()
    {
        if (!isTweening)
        {
            startPosition = targetCamera.position;
            StartCoroutine(TweenCamera());
        }
    }

    private IEnumerator TweenCamera()
    {
        isTweening = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            targetCamera.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetCamera.position = targetPosition; // Ensure it reaches the exact target position
        isTweening = false;
    }

    void Update()
    {        } 
}
