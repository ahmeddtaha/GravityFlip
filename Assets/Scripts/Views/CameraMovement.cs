using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves the camera horizontally to follow gameplay progression
public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed; // Speed at which the camera moves

    // Update is called once per frame
    void Update()
    {
        // Continuously move the camera to the right
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }
}
