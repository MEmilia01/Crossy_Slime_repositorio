using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasgroup : MonoBehaviour
{
    //esto sirve para que sea mas facil encontrar los objetos y evitar que se lien
    public GameObject menu;
    public GameObject puntuacion;
    public GameObject menumuerte;
    public GameObject pantallacarga;
    //aca se podra guardar la puntuacion
    public int num = 0;


    void Start()
    {
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
        //
    }
    public void Mostrarpuntuacion()
    {
        //muestra solo la puntuacion mientras se juega 
        menu.SetActive(false);
        puntuacion.SetActive(true);
        menumuerte.SetActive(false);
        pantallacarga.SetActive(false);
        //codigo para que valla contando cuando avance
        SceneManager.LoadScene("Generaciondelmapa");//se deberia poner el nombre aca de la scena
        //aca llama para que se ponga la scena del juego
    }
    public void Mostrarmenumuerte()
    {
        menu.SetActive(false);
        puntuacion.SetActive(false);
        menumuerte.SetActive(true);
        pantallacarga.SetActive(false);
        //que se vea cuando el jugador se "muera"
        //aca hay que poner el boton para la vuelta


    }
    public void Mostrarpantallacarga()
    {
        //esto es solo una pantalla de carga entre medias
        menu.SetActive(false);
        puntuacion.SetActive(false);
        menumuerte.SetActive(false);
        pantallacarga.SetActive(true);
    }
}