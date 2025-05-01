using UnityEngine;
using TMPro;

// Creates a floating popup when a coin is collected

public class ScorePopup : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;

    void Start()
    {
        // Destroy after a set time
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move popup upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}
