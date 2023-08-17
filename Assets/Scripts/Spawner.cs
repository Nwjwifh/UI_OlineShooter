using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBetweenSpawns;
    float nextSpawnTime;

    public GameObject enemy;

    public Transform[] spawnPoints;

    void Start()
    {
        // 초기에는 Spawner를 비활성화하여 적을 스폰하지 않도록 함
        enabled = false;
    }

    void Update()
    {
        if (enabled && Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
        }
    }

    public void DisableSpawner()
    {
        enabled = false;
    }
}
