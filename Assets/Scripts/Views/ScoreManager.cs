using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;   // ← New
    public GameObject gameOverPanel;

    public float score = 0f;
    public float scoreRate = 1f;
    private bool isGameOver = false;

<<<<<<< Updated upstream:Assets/Scripts/ScoreManager.cs
    void Start()
    {
        // Optional: Show current high score on launch
=======
    private GameAudioManager audioManager;


    void Start()
    {
        GameAudioManager.Instance.AddButtonSounds();

        score = 0f;
        isGameOver = false;

        audioManager = FindObjectOfType<GameAudioManager>();

>>>>>>> Stashed changes:Assets/Scripts/Views/ScoreManager.cs
        if (highScoreText != null)
        {
            int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + storedHighScore;
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += scoreRate * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int finalScore = Mathf.FloorToInt(score);
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update high score if needed
        if (finalScore > storedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.Save();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            GameAudioManager.Instance.AddButtonSounds(); 

        }
<<<<<<< Updated upstream:Assets/Scripts/ScoreManager.cs
=======

        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        if (audioManager != null)
        {
            audioManager.PlayGameOverSound();
            audioManager.PlayMenuMusic(); 
        }
>>>>>>> Stashed changes:Assets/Scripts/Views/ScoreManager.cs
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // resume time if paused
        SceneManager.LoadScene("MainMenu"); // replace with exact name of your main menu scene
    }


}
