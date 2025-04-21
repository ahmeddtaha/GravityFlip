using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;
    public float difficultyIncreaseInterval = 10f;     // every 10 seconds
    public float spawnRateMultiplier = 0.9f;           // 10% faster each step
    public float minSpawnInterval = 0.6f;              // never too spammy

    private float timer;

    void Start()
    {
        timer = difficultyIncreaseInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            IncreaseDifficulty();
            timer = difficultyIncreaseInterval;
        }
    }

    void IncreaseDifficulty()
    {
        obstacleSpawner.spawnInterval = Mathf.Max(
            obstacleSpawner.spawnInterval * spawnRateMultiplier,
            minSpawnInterval
        );

        Debug.Log("Difficulty Increased — New Interval: " + obstacleSpawner.spawnInterval);
    }
}
