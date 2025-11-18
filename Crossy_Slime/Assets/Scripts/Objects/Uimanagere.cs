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
    public float num;
    
    [SerializeField] Movement player;

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
        //Con puntuaciontexto se pone el texto por pantalla
        //y en counter esta el contador que cuanta por fila


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
    }
}