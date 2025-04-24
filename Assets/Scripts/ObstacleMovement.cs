using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;


    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < Camera.main.transform.position.x - 20f)
        {
            Destroy(gameObject);
        }
    }
}
