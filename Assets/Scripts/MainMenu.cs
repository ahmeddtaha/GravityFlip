using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreDisplay;

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
