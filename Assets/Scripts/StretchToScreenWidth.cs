using UnityEngine;

[ExecuteAlways]
public class StretchToScreenWidth : MonoBehaviour
{
    public Camera mainCam;

    void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;

        ResizeToScreenWidth();
    }

    void ResizeToScreenWidth()
    {
        float screenHalfWidthInWorld = mainCam.orthographicSize * Screen.width / Screen.height;
        Vector3 newScale = transform.localScale;
        newScale.x = screenHalfWidthInWorld * 2f + 2f; // Add some buffer
        transform.localScale = newScale;
    }
}
