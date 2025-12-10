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
    public float speedStandard = 2;
    public float fast = 5;
    public bool enrango = true;
    public bool empezar;

    public float duration = 1f;

    public void Inicio()
    {
        speed = speedStandard;
        cameramove = this;
    }
    void Update()
    {
        Camara();
    }

    void Camara()
    {
        if (distancia < -2)
            empezar = true;
        
        if (empezar)
        {
            transform.position = transform.position + new Vector3(0, 0, Time.deltaTime * speed);
        }
        //if (distancia < -3) { back(); }
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

        if (distancia < -7)
        {
            if (enrango)
            {
                Rapidez();
                enrango = false;
            }
        }
        else
        {
            if (!enrango)
            {
                BackToNormal();
                enrango = true;
            }
        }

        if (distancia > 4)
        {
            empezar = false;
            Cursor.lockState = CursorLockMode.None;
            grifo.SetActive(true);
            grifo.transform.position = player.transform.position + new Vector3(5, -1, 0); //por alguan razon se desajusta y con este vector es facil de ajustar
            Dead.dead.IsDead();
        }
    }

    void Rapidez()
    {
        DOTween.To(() => speed, x => speed = x, fast, 0.2f);
    }

    void BackToNormal()
    {
        DOTween.To(() => speed, x => speed = x, speedStandard, 0.2f);
    }
}
