using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 15f;
    public float minY = -3f;
    public float maxY = 3f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnObstacle", 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        if (player == null) return;

        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, yPos, 0f);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
