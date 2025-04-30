using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/MainMenu.cs
    public TextMeshProUGUI highScoreDisplay;
=======
    public Button startButton;
    public Button leaderboardButton;
    public Button logoutButton;

    private void Start()
    {
        // Play menu ambient music
        GameAudioManager.Instance.PlayMenuMusic();
        GameAudioManager.Instance.AddButtonSounds(); 


        // Setup button listeners
        startButton.onClick.AddListener(StartGame);
        leaderboardButton.onClick.AddListener(ShowLeaderboard);
        logoutButton.onClick.AddListener(Logout);

        // Check if user is logged in
        string authToken = PlayerPrefs.GetString("AuthToken", "");
        if (string.IsNullOrEmpty(authToken))
        {
            // If not logged in, go to login scene
            SceneManager.LoadScene("Login");
        }
    }
>>>>>>> Stashed changes:Assets/Scripts/Controllers/MainMenu.cs

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // replace with actual game scene name
    }

    public void ShowHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreDisplay.text = "High Score: " + highScore;
        highScoreDisplay.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit (won’t work in editor)");
    }
}
