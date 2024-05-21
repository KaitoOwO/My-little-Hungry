using UnityEngine;

public class SpawnObstaculos : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 30f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + spawnDistance, 0, transform.position.z);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
