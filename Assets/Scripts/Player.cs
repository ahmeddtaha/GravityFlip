using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float flipForce = 10f;

    private Rigidbody2D rb;
    private bool isGravityFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }

    void Update()
    {
        // Only flip gravity
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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;

            // Tell ScoreManager to stop counting
            FindObjectOfType<ScoreManager>().GameOver();
        }
    }



}
