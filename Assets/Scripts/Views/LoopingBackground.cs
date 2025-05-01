using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scrolls the background texture to simulate movement

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer backgroundRenderer;


    // Update is called once per frame
    void Update()
    {
        // Move the texture offset to create a scrolling effect
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
