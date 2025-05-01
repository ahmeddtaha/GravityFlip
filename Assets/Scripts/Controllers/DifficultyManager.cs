using UnityEngine;

// Gradually increases game difficulty by reducing the spawn interval between obstacles
public class DifficultyManager : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;
    public float difficultyIncreaseInterval = 10f;     // every 10 seconds
    public float spawnRateMultiplier = 0.9f;           // 10% faster each step
    public float minSpawnInterval = 0.6f;              // Minimum allowed spawn interval (to prevent spamming)

    private float timer;

    void Start()
    {
        // Start the countdown from the defined interval
        timer = difficultyIncreaseInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Every interval, increase the game's difficulty
        if (timer <= 0f)
        {
            IncreaseDifficulty();
            timer = difficultyIncreaseInterval;
        }
    }

    void IncreaseDifficulty()
    {
        // Reduce the spawn interval using the multiplier,but don't go below the minimum value
        obstacleSpawner.spawnInterval = Mathf.Max(
            obstacleSpawner.spawnInterval * spawnRateMultiplier,
            minSpawnInterval
        );

        Debug.Log("Difficulty Increased — New Interval: " + obstacleSpawner.spawnInterval);
    }
}
