using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// Manages button navigation and music in the main menu
public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button leaderboardButton;
    public Button logoutButton;

    private void Start()
    {
        // Play menu ambient music and button sound effect
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