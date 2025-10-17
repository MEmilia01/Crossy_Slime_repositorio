using UnityEngine;

public class canvasgroup : MonoBehaviour
{
    public GameObject[] objetos; // Array con todos los objetos del Canvas
    private int indiceActual = 0;

    void Start()
    {
        MostrarMenu(indiceActual);
    }
    void MostrarMenu(int indice)
    {
        //el menu siempre sera el primero de la lista, por eso 
        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i].SetActive(i == indice); // Solo activa el objeto actual
        }
    }
  /*  public void MostrarSiguiente()
    {
        indiceActual = (indiceActual + 1) % objetos.Length; // Avanza circularmente
        MostrarMenu(indiceActual);
    }
  */
    
}