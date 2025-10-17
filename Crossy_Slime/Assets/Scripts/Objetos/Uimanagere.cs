using UnityEngine;

public class canvasgroup : MonoBehaviour
{
    public GameObject[] menus; // Array con todos los objetos del Canvas
    private int indiceActual = 0;

    void Start()
    {
        MostrarMenu(indiceActual);
    }
    void MostrarMenu(int indice)
    {
        //el menu siempre sera el primero de la lista, por eso 
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == indice); // Solo activa el objeto actual
        }
    }
    public void Mostrarpuntuacion(int indice)
    {
        //cuando se inicia el juego se muestra 
        
    }
 
    
}