using DG.Tweening;
using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Mesh meshDragonArriba;
    [SerializeField] Mesh meshDragonAbajo;
    [SerializeField] MeshFilter meshFilterDragon;

    [SerializeField] float flapInterval = 8f;
    [SerializeField] float flapHeight = 2f;
    [SerializeField] float flapDuration = 2f;

    [SerializeField] float speedDragon = 10f;

    Sequence currentFlapSequence;

    void Start()
    {
        // Inicializa DOTween explicitamente y espera un frame si es necesario
        DOTween.Init(false, false, LogBehaviour.Verbose);

        // Opcional: asegurar que el primer frame ya paso (evita edge cases con transform.position)
        StartCoroutine(InitAfterFrame());
    }

    IEnumerator InitAfterFrame()
    {
        yield return null; // Espera al menos un frame
        ResetPosition();
        AudioManager.Instance.Dragon();
        StartCoroutine(FlapRoutine());
    }

    void Update()
    {
        transform.position -= transform.right * speedDragon * Time.deltaTime;
    }

    IEnumerator FlapRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flapInterval);
            Flap();
        }
    }

    void Flap()
    {
        // Cancelar flap anterior (seguro, incluso si es null)
        currentFlapSequence.Kill();

        AudioManager.Instance.Dragon();
        meshFilterDragon.mesh = meshDragonAbajo;
        // Guardar posicion Y inicial para el ciclo completo
        float startY = transform.position.y;

        // Usa callbacks con captura segura y SetTarget
        currentFlapSequence = DOTween.Sequence()
            .Append(transform.DOMoveY(startY + flapHeight, flapDuration * 0.5f)
                .SetEase(Ease.OutSine))
            .AppendCallback(() =>
            {
                meshFilterDragon.mesh = meshDragonArriba;
            })
            .Append(transform.DOMoveY(startY, flapDuration * 0.5f)
                .SetEase(Ease.InSine)); // tambien en la secuencia global 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == endPoint)
        {
            StopAllCoroutines(); // detiene FlapRoutine
            ResetPosition();
            StartCoroutine(FlapRoutine()); // reinicia el ciclo
        }
    }
    void ResetPosition()
    {
        currentFlapSequence.Kill();
        transform.position = spawnPoint.position;
        meshFilterDragon.mesh = meshDragonArriba;
    }
}