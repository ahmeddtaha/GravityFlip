using System.Collections;
using UnityEngine;

// Spawns obstacles and coins ahead of the player at random vertical positions

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // The obstacle prefab to spawn
    public GameObject coinPrefab; // The coin prefab to spawn
    public float spawnInterval = 2f;
    public float spawnDistance = 15f;
    public float minY = -3f;
    public float maxY = 3f;

    private Transform player; // Reference to the player’s transform

    void Start()
    {
        // Find the player in the scene using tag and begin the spawn loop
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Spawn obstacle and possibly coin
            SpawnObstacle();

            // Ensure spawnInterval is valid, then wait before next spawn
            yield return new WaitUntil(() => spawnInterval > 0);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacle()
    {
        if (player == null) return;

        // Choose a random vertical position within the defined range
        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, yPos, 0f);
        
        // Instantiate an obstacle ahead of the player
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // 30% chance to also spawn a coin slightly ahead of the obstacle
        if (Random.value < 0.3f) 

        {
            Vector3 coinPosition = new Vector3(player.position.x + spawnDistance + 5f, Random.Range(minY, maxY), 0f);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }


    }
}
