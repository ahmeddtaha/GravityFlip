using UnityEngine;

// Controls coin movement, collision detection, and score handling

public class Coin : MonoBehaviour
{
    public int scoreValue = 5;
    public float moveSpeed = 5f;
    public AudioClip collectSound;
    public GameObject scorePopupPrefab;



    void Update()
    {
        // Move the coin left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Destroy if it goes off-screen
        if (transform.position.x < Camera.main.transform.position.x - 20f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If player collects the coin
        if (other.CompareTag("Player"))
        {
            // Add score using ScoreManager
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            // Show popup and play sound
            Instantiate(scorePopupPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            
            Destroy(gameObject);
        }
    }
}
