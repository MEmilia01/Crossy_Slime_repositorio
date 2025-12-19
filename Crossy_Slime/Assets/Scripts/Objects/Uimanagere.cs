using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class Uimanagere : MonoBehaviour
{
    //esto sirve para que sea mas facil encontrar los objetos y evitar que se lien
    public GameObject menu;
    public GameObject puntuacion;
    public GameObject menumuerte;

    public GameObject Logosonido;
    public GameObject Nologosonido;

    //aca se podra guardar la puntuacion
    public float num;
    public bool Volumen = true;
  

    [SerializeField] Movement player;
    public Cameramove camera;
    public ScoreManager scoreman;
    [SerializeField] AudioManager audiomanager;


    string Juegobase;

    void Start()
    {
        Juegobase = SceneManager.GetActiveScene().name;

        int volumenGuardado = PlayerPrefs.GetInt("VolumenActivo", 1); // 1 = true por defecto
        Volumen = volumenGuardado == 1;

        if (Volumen)
        {
            Logosonido.SetActive(true);
            Nologosonido.SetActive(false);
            audiomanager.UnMute();
        }
        else
        {
            Logosonido.SetActive(false);
            Nologosonido.SetActive(true);
            audiomanager.MuteAll();
        }
        num = 0;
    }
    public void MostrarMenu()
    {
        //el menu siempre sera el primero de la lista, por eso 
        menu.SetActive(true);
        puntuacion.SetActive(false);
        menumuerte.SetActive(false);
        player.enabled = false;
        CargaMenu.cm.CargarMenuCarga();
    }
    public void Mostrarpuntuacion()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //muestra solo la puntuacion mientras se juega 
        menu.SetActive(false);
        puntuacion.SetActive(true);
        menumuerte.SetActive(false);
        //y en counter esta el contador 
        scoreman.restart(); 
        camera.Inicio();
        player.enabled = true; // para que el jugador sea movible
        num = 1; // esto activa la camar
    }
    public void Mostrarmenumuerte()
    {
        menu.SetActive(false);
        puntuacion.SetActive(false);
        menumuerte.SetActive(true);
        //que se vea cuando el jugador se "muera"
        //aca hay que poner el boton para la 

        //SceneManager.LoadScene(Juegobase);
        //MostrarMenu();

    }

    
    public void Sonido()
    {
        Volumen = !Volumen;

        if (Volumen)
        {
            Logosonido.SetActive(true);
            Nologosonido.SetActive(false);
            audiomanager.UnMute();
        }
        else
        {
            Logosonido.SetActive(false);
            Nologosonido.SetActive(true);
            audiomanager.MuteAll();
        }

        PlayerPrefs.SetInt("VolumenActivo", Volumen ? 1 : 0);
        PlayerPrefs.Save(); 
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}