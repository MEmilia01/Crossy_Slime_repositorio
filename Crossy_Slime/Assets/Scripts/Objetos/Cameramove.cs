using UnityEngine;
using DG.Tweening;

public class Cameramove : MonoBehaviour
{
    public GameObject player;
    public Transform targetCamera;

    public float speed;
    public float principio = 2;
    public float masinicio = 5;

    public float duration = 1f;


    void Start()
    {
        
        speed = principio;
    }
    void Update()
    {

        transform.position = transform.position + new Vector3(0, 0, Time.deltaTime * speed);
        Debug.Log("temueves, camara");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rapidez();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            back();
        }

    }  
    
    void Rapidez()
    {
        DOTween.To(() => speed, x => speed = x, masinicio, 0.2f);
    }

    void back()
    {
        DOTween.To(() => speed, x => speed = x, principio, 0.2f);
    }
}
