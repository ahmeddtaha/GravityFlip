using UnityEngine;

// Controls obstacle movement and destruction

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;


    void Update()
    {
        // Move obstacle leftward
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Destroy when it goes off-screen
        if (transform.position.x < Camera.main.transform.position.x - 20f)
        {
            Destroy(gameObject);
        }
    }
}
