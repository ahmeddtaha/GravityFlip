using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages score, high score, game over state, and leaderboard submission

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
        // Add button click sounds and start music
        GameAudioManager.Instance.AddButtonSounds();

        score = 0f;
        isGameOver = false;

        audioManager = FindObjectOfType<GameAudioManager>();

        if (highScoreText != null)
        {
            int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + storedHighScore;
        }

        // Play game music
        if (audioManager != null)
        {
            audioManager.PlayGameplayMusic();
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Increase and display score
            score += scoreRate * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void AddScore(int amount)
    {
        // Add score from coins
        score += amount;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        int finalScore = Mathf.FloorToInt(score);
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Save high score if it's better
        if (finalScore > storedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.Save();
        }

        // Submit score to server
        StartCoroutine(NetworkManager.Instance.SubmitScore(finalScore, (success, message) =>
        {
            Debug.Log(message);
        }));

        // Update high score display
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }

        // Show Game Over screen
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            GameAudioManager.Instance.AddButtonSounds(); 

        }

        // Hide pause button
                if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }
  
                // Play sounds
        if (audioManager != null)
        {
            audioManager.PlayGameOverSound();
            audioManager.PlayMenuMusic(); 
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        // Restart music and reload the scene
        if (audioManager != null)
        {
            audioManager.RestartMusic();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}