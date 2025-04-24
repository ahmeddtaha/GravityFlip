using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreDisplay;
    public Button startButton;
    public Button leaderboardButton;
    public Button logoutButton;

    private void Start()
    {
        // Setup button listeners
        startButton.onClick.AddListener(StartGame);
        leaderboardButton.onClick.AddListener(ShowLeaderboard);
        logoutButton.onClick.AddListener(Logout);

        // Show high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreDisplay.text = "High Score: " + highScore;

        // Check if user is logged in
        string authToken = PlayerPrefs.GetString("AuthToken", "");
        if (string.IsNullOrEmpty(authToken))
        {
            // If not logged in, go to login scene
            SceneManager.LoadScene("Login");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void Logout()
    {
        NetworkManager.Instance.Logout();
        SceneManager.LoadScene("Login");
    }
}