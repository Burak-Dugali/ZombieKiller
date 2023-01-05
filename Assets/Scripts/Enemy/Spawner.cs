using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] m_SpawnPoints;
    public GameObject m_EnemyPrefab;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawnEnemy();
        }
    }

    void OnEnable()
    {
        ZombieStats.OnEnemyKilled += spawnEnemy;
    }

    void spawnEnemy()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0f,m_SpawnPoints.Length-1));

        Instantiate(m_EnemyPrefab, m_SpawnPoints[randomNumber].transform.position,Quaternion.identity);
    }
}
