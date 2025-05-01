using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

// Manages fetching and displaying leaderboard scores from the server

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardEntries; // Array of 4 text fields
    public TextMeshProUGUI loadingText;
    public Button refreshButton;
    public Button backButton;

    private void Start()
    {
        // Button setup and initial refresh
        refreshButton.onClick.AddListener(RefreshLeaderboard);
        backButton.onClick.AddListener(BackToMainMenu);
        GameAudioManager.Instance.AddButtonSounds();
        RefreshLeaderboard();
    }

    private void RefreshLeaderboard()
    {
        loadingText.gameObject.SetActive(true);
        ClearLeaderboard();

        // Fetch leaderboard from server
        StartCoroutine(NetworkManager.Instance.GetLeaderboard((success, entries) =>
        {
            loadingText.gameObject.SetActive(false);

            if (success && entries != null)
            {
                // Sort entries by score in descending order
                var sortedEntries = entries.OrderByDescending(e => e.high_score).ToArray();

                // Display up to 10 entries
                for (int i = 0; i < Mathf.Min(sortedEntries.Length, leaderboardEntries.Length); i++)
                {
                    var entry = sortedEntries[i];
                    leaderboardEntries[i].text = $"{i + 1}. {entry.username}: {entry.high_score}";
                }
            }
            else
            {
                loadingText.text = "Failed to load leaderboard";
            }
        }));
    }

    private void ClearLeaderboard()
    {
        // Clear text from all entry fields
        foreach (var entry in leaderboardEntries)
        {
            entry.text = "";
        }
    }

    private void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}