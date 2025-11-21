using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Security.Cryptography;

public class Cameramove : MonoBehaviour
{
    public GameObject player;
    public Transform targetCamera;
   
    public float distancia;
    public float speed;
    public float principio;
    public float masinicio;
    public bool enrango = true;

    public float duration = 1f;

    public Uimanagere referenciaui;
    //public Casilla referenciamuerteI;
    public Movement referenciamuerteII;

    public void Inicio()
    {
        enrango = true;
        speed = principio;
    }
    //nunca invocar desde el update, ya que resetearia 

    void Start()
    {  
     
        
    }
    void Update()
    {

        transform.position += new Vector3(0, 0, Time.deltaTime * speed);
        //transform.position = playerposition.position + offset;


        if (Input.GetKeyDown(KeyCode.C))
            Inicio();


        if(Input.GetKeyDown(KeyCode.R))
        {
            Rapidez();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            back();
        }
        
        distancia = transform.position.z - player.transform.position.z;
        //tiene que estar entre 1 y -9
        


        if(distancia < -5 && enrango)
        {
            enrango = false;

                Rapidez();
            
        }
        else if (distancia > -5 && !enrango)
        {
            enrango = true;
            back();
        }



        // para que detenga la camara cuando muere
        if (distancia > 1)
        {
            stop();
            player.GetComponent<Movement>().enabled = false;
        }

        //esto hace que la camara se detenga
       /* if (referenciamuerteI.enabled == false)
        {
            stop();
        }
       */
        if (referenciamuerteII.enabled == false)
        {
            stop();
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
    void stop()
    {
        speed = 0;        
    }
}
