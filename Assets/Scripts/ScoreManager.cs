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

    void Start()
    {
        // Optional: Show current high score on launch
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
        }
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
