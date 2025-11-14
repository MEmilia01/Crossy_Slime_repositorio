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
    public bool empezar = false;

    public float duration = 1f;
    

    void Inicio()
    {
        enrango = true;
        speed = principio;
    }

    void Start()
    {  
     
        //necesito que el menu mande una señal, con el cual se inicie la camara
        
    }
    void Update()
    {
        if (empezar == true)
        {
            Inicio();
            empezar = false;
        }

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
        
        if(distancia < -6)
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
        if(distancia < -3) { back(); }

    }

    void back()
    {
        DOTween.To(() => speed, x => speed = x, principio, 0.2f);
    }
}
