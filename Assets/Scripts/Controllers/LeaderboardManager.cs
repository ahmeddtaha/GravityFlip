using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardEntries; // Array of 10 TextMeshProUGUI elements
    public TextMeshProUGUI loadingText;
    public Button refreshButton;
    public Button backButton;

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshLeaderboard);
        backButton.onClick.AddListener(BackToMainMenu);
        GameAudioManager.Instance.AddButtonSounds();
        RefreshLeaderboard();
    }

    private void RefreshLeaderboard()
    {
        loadingText.gameObject.SetActive(true);
        ClearLeaderboard();

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