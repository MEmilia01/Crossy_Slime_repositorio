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

    public Uimanagere referenciaui;
    public Casilla referenciamuerteI;
    public Movement referenciamuerteII;

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
        if (referenciaui.num > 0)
        { Inicio(); }

        transform.position = transform.position + new Vector3(0, 0, Time.deltaTime * speed);
       //transform.position = playerposition.position + offset;
        

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
