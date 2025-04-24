using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance { get; private set; }

    public AudioClip gameplayMusic;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayGameplayMusic();
    }

    public void PlayGameplayMusic()
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverSound);
    }

    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void RestartMusic()
    {
        PlayGameplayMusic();
    }

    // Call this method in Start() of each scene to add sound to all buttons
    public void AddButtonSounds()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonClick);
        }
    }
}