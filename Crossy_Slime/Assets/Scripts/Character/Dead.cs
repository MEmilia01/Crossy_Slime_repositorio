using UnityEngine;
using System.Collections.Generic;

public class Dead : MonoBehaviour
{
    public GameObject player;
    public GameObject dragon;
    public GameObject grifo;
    public Cameramove camara;
    public GameObject casillaMuerte;
    public GameObject menuPuntuacion;
    public GameObject menuMuerte;
    public static Dead dead;
    public ScoreManager scoreManager;

    private void Start()
    {
        grifo.SetActive(false);
    }

    private void Awake()
    {
        dead = GetComponent<Dead>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject == dragon)
        //{
        //    Debug.Log("Dead by dragon");
        //    //IsDead();
        //}
        if (collision.gameObject == grifo)
        {
            Debug.Log("Dead by Grifo");
            player.SetActive(false);
        }
    }

    public void IsDead()
    {
        camara.Stop();
        player.GetComponent<Movement>().enabled = false;
        menuPuntuacion.SetActive(false);
        menuMuerte.SetActive(true);
        scoreManager.GameCompleted();
    }
}
