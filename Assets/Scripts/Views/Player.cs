using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

// Handles gravity flipping, player input, and collision with obstacles

public class Player : MonoBehaviour
{
    public float flipForce = 10f;
    public GameObject flipParticles;
    private Rigidbody2D rb;
    private bool isGravityFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // Start with gravity pulling down
    }

    void Update()
    {
        // Flip gravity on space or mouse click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        isGravityFlipped = !isGravityFlipped;

        // Invert gravity
        rb.gravityScale *= -1;

        // Flip sprite visually
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;

        // Smooth vertical force
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, flipForce * (isGravityFlipped ? 1 : -1)), ForceMode2D.Impulse);

        // Instantiate particle effect
        if (flipParticles != null)
        {
            // Instantiate the particle prefab and store a reference to it
            GameObject particles = Instantiate(flipParticles, transform.position, Quaternion.identity);

            // Destroy after short duration (optional, if your system doesn't auto-destroy)
            Destroy(particles, 0.5f);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Game over if player hits an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;

            // Tell ScoreManager to stop counting
            FindObjectOfType<ScoreManager>().GameOver();
        }
    }



}
