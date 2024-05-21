using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; // Array de prefabs de obstáculos
    public Transform spawnPoint; // Punto donde se generan los obstáculos
    public float minSpawnTime = 1f; // Tiempo mínimo entre spawns
    public float maxSpawnTime = 2f; // Tiempo máximo entre spawns

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[randomIndex], spawnPoint.position, Quaternion.identity);
    }
}