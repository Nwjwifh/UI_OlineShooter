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
        // �ʱ⿡�� Spawner�� ��Ȱ��ȭ�Ͽ� ���� �������� �ʵ��� ��
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
