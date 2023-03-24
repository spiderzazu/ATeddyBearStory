using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public GameObject[] activeEnemies;

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            activeEnemies[i] = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }

    public void ReActivateEnemies()
    {
        for (int i = 0; i < activeEnemies.Length; i++)
        {
            if (!activeEnemies[i].gameObject.activeSelf)
                activeEnemies[i].SetActive(true);
        }
    }
}
