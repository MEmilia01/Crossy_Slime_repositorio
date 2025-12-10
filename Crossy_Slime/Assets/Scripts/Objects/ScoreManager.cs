using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    public int currentScore = 0;            // �ltimo "index" alcanzado (fila m�s alta)
    public int highScore = 0;               // R�cord personal (guardado)

    [Header("UI")]
    public TMP_Text scoreText;                // Componente TextUI (personalizado)
    public TMP_Text highScoreText;            // Para mostrar r�cord separado
    public TMP_Text actualScoreForDead;
    void Start()
    {
        LoadHighScore();
        UpdateUI();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    ResetHighScore();
        //}
    }


    public void restart() 
    { 
        currentScore = score;
        scoreText.text = currentScore.ToString("0");
    }
    // Se llama cada vez que el jugador llega a una nueva fila (index = n� de fila)
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
        // Guardar nuevo r�cord si supera el anterior
        if (currentScore > highScore)
        {
            SaveHighScore();
        }
        actualScoreForDead.text = currentScore.ToString("0");
        // Mostrar mensaje
        Debug.Log($"Juego terminado. Puntuaci�n: {currentScore}, R�cord: {highScore}");
    }

    // Reinicia la puntuaci�n actual para una nueva partida
    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    // --- Gesti�n de persistencia ---
    private void SaveHighScore()
    {
        highScore = currentScore;
        Debug.Log(highScore);
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
        UpdateUI();

    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // --- Actualizaci�n de UI ---
    public void UpdateUI()
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