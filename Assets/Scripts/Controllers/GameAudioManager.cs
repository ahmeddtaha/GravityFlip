using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages music and sound effects across all scenes using Singleton

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance { get; private set; }

    public AudioClip gameplayMusic;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;
    public AudioClip menuMusic;


    private AudioSource audioSource;

    void Awake()
    {
        // Ensure only one instance persists across scenes
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
        PlayGameplayMusic(); // Default music on start
    }

    public void PlayGameplayMusic()
    {
        // Avoid restarting if already playing
        if (audioSource.clip == menuMusic && audioSource.isPlaying)
            return;
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
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

    public void PlayMenuMusic()
    {
        if (audioSource.clip == menuMusic && audioSource.isPlaying)
            return;

        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Adds click sounds to all buttons in the scene
    public void AddButtonSounds()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonClick);
        }
    }
}