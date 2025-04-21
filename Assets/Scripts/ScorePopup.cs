using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}
