using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{
    public AudioClip gameplayMusic;
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void RestartMusic()
    {
        PlayGameplayMusic();
    }
}
