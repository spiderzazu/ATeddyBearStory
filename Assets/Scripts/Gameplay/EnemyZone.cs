using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    public GameObject enemyMainBody;
    private float timeInZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyMainBody.GetComponent<BearEnemyController>().Attack(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timeInZone += Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && timeInZone > 3.5f)
        {
            enemyMainBody.GetComponent<BearEnemyController>().Attack(other.gameObject);
            timeInZone = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeInZone = 0f;
        }
    }
}
