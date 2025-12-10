using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Uimanagere : MonoBehaviour
{
    //esto sirve para que sea mas facil encontrar los objetos y evitar que se lien
    public GameObject menu;
    public GameObject puntuacion;
    public GameObject menumuerte;
    public GameObject pantallacarga;
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
        MostrarMenu();
        num = 0;
    }
    public void MostrarMenu()
    {
        //el menu siempre sera el primero de la lista, por eso 
        menu.SetActive(true);
        puntuacion.SetActive(false);
        menumuerte.SetActive(false);
        pantallacarga.SetActive(false);
        player.enabled = false;
    }
    public void Mostrarpuntuacion()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //muestra solo la puntuacion mientras se juega 
        menu.SetActive(false);
        puntuacion.SetActive(true);
        menumuerte.SetActive(false);
        pantallacarga.SetActive(false);
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
        pantallacarga.SetActive(false);
        //que se vea cuando el jugador se "muera"
        //aca hay que poner el boton para la 

    }
    public void Mostrarpantallacarga()
    {
        //esto es solo una pantalla de carga entre medias
        menu.SetActive(false);
        puntuacion.SetActive(false);
        menumuerte.SetActive(false);
        pantallacarga.SetActive(true);
        // hay que hacer que espere unos segundos
        Debug.Log("imagen");
        SceneManager.LoadScene(Juegobase);
        MostrarMenu();
    }
    public void Sonido()
    {
        if (Volumen != false)
        {
            Logosonido.SetActive(false);
            Nologosonido.SetActive(true);
            Volumen = false;
            audiomanager.MuteAll();
        }
        else if (Volumen != true)
        {
            Logosonido.SetActive(true);
            Nologosonido.SetActive(false);
            Volumen = true;
            audiomanager.UnMute();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}