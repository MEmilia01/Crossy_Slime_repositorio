using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int puntuacionMax;
    internal bool havePlayerMoved;
    internal bool isAvailableMove = false;

    public void NewIndex(int index)
    {
        if (index > puntuacionMax)
        {
            puntuacionMax = index;
        }
    }

    // Se llama cuando el personaje muere
    public void GameCompleted()
    {
        if (puntuacionMax > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.GetInt("HighScore", puntuacionMax);
            PlayerPrefs.Save();
        }

    }
}