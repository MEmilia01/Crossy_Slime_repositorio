using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class Cameramove : MonoBehaviour
{
    public GameObject player;
    public Transform targetCamera;
   
    public float distancia;
    public float speed;
    public float principio = 2;
    public float masinicio = 5;
    public bool enrango = true;

    public float duration = 1f;


    void Start()
    {   
       enrango = true;
       speed = principio;
    }
    void Update()
    {

        transform.position = transform.position + new Vector3(0, 0, Time.deltaTime * speed);
       //transform.position = playerposition.position + offset;
        

        if(Input.GetKeyDown(KeyCode.R))
        {
            Rapidez();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            back();
        }
        
        distancia = transform.position.z - player.transform.position.z;
        //tiene que estar entre 1 y -9
        
        if(distancia < -8)
        {
            enrango = false;
            if(enrango != true)
            {
                Rapidez();
            }
        }
        else
        {
            enrango = true;
        }
        
        

        /*
        if(player.GetComponent<inputActive>)
        {
            //camara se queda quieta
        }
        */
    }  
    
    void Rapidez()
    {
        DOTween.To(() => speed, x => speed = x, masinicio, 0.2f);

        enrango = true;
        if(distancia < -8) { back(); }

    }

    void back()
    {
        DOTween.To(() => speed, x => speed = x, principio, 0.2f);
    }
}
