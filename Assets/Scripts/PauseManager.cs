using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;


    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true); // 👈 Show pause button
    }


    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false); // 👈 Hide pause button
    }



    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace with your actual scene name
    }
}
