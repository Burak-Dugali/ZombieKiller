using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public Transform[] m_SpawnPoints;
    public GameObject m_HealthPrefab;

    void OnEnable()
    {
        ZombieStats.OnEnemyKilled += spawnHealth;
    }

    void spawnHealth()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0f, m_SpawnPoints.Length - 1));

        Instantiate(m_HealthPrefab, m_SpawnPoints[randomNumber].transform.position, Quaternion.identity);
    }
}
