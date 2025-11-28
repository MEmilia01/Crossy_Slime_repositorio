using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class Cameramove : MonoBehaviour
{
    public static Cameramove cameramove;
    public GameObject player;
    public GameObject grifo;
    public Transform targetCamera;
    public Canvas canvas;

    public float distancia;
    public float speed;
    public float principio = 2;
    public float masinicio = 5;
    public bool enrango = true;


    public float duration = 1f;

    public void Inicio()
    {
        enrango = true;
        speed = principio;
    }
    
    void Update()
    {
        if (enrango == true)
        {
            transform.position = transform.position + new Vector3(0, 0, Time.deltaTime * speed);
            //transform.position = playerposition.position + offset;
        }

        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    Rapidez();
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    back();
        //}
        
        distancia = transform.position.z - player.transform.position.z;
        //tiene que estar entre 1 y -9
        
        if(distancia < -7)
        {
            enrango = false;
            if(enrango != true)
            {
                Rapidez();
            }
        }
        if (distancia < -3) { back(); }
        else
        {
            enrango = true;
        }

        if (distancia > 4)
        {
            stop();
            grifo.SetActive(true);
            grifo.transform.position = player.transform.position + new Vector3(5, -1, 0); //por alguan razon se desajusta y con este vector es facil de ajustar
            Dead.dead.IsDead();
        }

    }

    void Rapidez()
    {
        DOTween.To(() => speed, x => speed = x, masinicio, 0.2f);
        enrango = true;
    }

    void back()
    {
        DOTween.To(() => speed, x => speed = x, principio, 0.2f);
    }
    public void stop()
    {
        speed = 0;
    }
}
