using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;            // Último "index" alcanzado (fila más alta)
    public int highScore = 0;               // Récord personal (guardado)

    [Header("UI")]
    public TMP_Text scoreText;                // Componente TextUI (personalizado)
    public TMP_Text highScoreText;            // Para mostrar récord separado

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
            scoreText.text = currentScore.ToString("0");
        }

        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString("0");
        }
    }

    // Reinicia el high score a 0 (guardado y en memoria)
    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();

        Debug.Log("High score reiniciado a 0.");
        UpdateUI(); // Opcional: actualiza el texto en pantalla
    }
}