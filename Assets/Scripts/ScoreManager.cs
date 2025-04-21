using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public GameObject pauseButton;

    private float score = 0f;
    private float scoreRate = 1f;
    private bool isGameOver = false;

    private GameAudioManager audioManager;

    void Start()
    {
        score = 0f;
        isGameOver = false;

        audioManager = FindObjectOfType<GameAudioManager>();

        if (highScoreText != null)
        {
            int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + storedHighScore;
        }

        if (audioManager != null)
        {
            audioManager.PlayGameplayMusic();
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            score += scoreRate * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int finalScore = Mathf.FloorToInt(score);
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);

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
        }

        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        if (audioManager != null)
        {
            audioManager.PlayGameOverSound();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (audioManager != null)
        {
            audioManager.RestartMusic();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
