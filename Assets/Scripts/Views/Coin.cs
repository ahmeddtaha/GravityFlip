using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 5;
    public float moveSpeed = 5f;
    public AudioClip collectSound;
    public GameObject scorePopupPrefab;



    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < Camera.main.transform.position.x - 20f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            Instantiate(scorePopupPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            Destroy(gameObject);
        }
    }
}
