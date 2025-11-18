using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;            // Último "index" alcanzado (fila más alta)
    public int highScore = 0;               // Récord personal (guardado)

    [Header("UI")]
    public Text scoreText;                // Componente TextUI (personalizado)
    public Text highScoreText;            // Para mostrar récord separado

    void Start()
    {
        LoadHighScore();
        UpdateUI();
    }

    // Se llama cada vez que el jugador llega a una nueva fila (index = nº de fila)
    public void NewIndex(int index)
    {
        if (index > currentScore)
        {
            currentScore = index;
            UpdateUI(); // Actualiza texto en pantalla
        }
    }

    // Se llama cuando el personaje muere o termina la partida
    public void GameCompleted()
    {
        // Guardar nuevo récord si supera el anterior
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }

        // Mostrar mensaje
        Debug.Log($"Juego terminado. Puntuación: {currentScore}, Récord: {highScore}");
    }

    // Reinicia la puntuación actual para una nueva partida
    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    // --- Gestión de persistencia ---
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
#if UNITY_EDITOR
        Debug.Log($"Nuevo récord guardado: {highScore}");
#endif
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // --- Actualización de UI ---
    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }
}