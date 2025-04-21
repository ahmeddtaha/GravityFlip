using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 15f;
    public float minY = -3f;
    public float maxY = 3f;
    public GameObject coinPrefab;


    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnObstacle();

            // Wait for dynamic interval (re-evaluated every loop)
            yield return new WaitUntil(() => spawnInterval > 0);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacle()
    {
        if (player == null) return;

        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, yPos, 0f);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        if (Random.value < 0.3f) // 30% chance
        {
            Vector3 coinPosition = new Vector3(player.position.x + spawnDistance + 5f, Random.Range(minY, maxY), 0f);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }


    }
}
