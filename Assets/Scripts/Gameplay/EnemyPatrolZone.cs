using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolZone : MonoBehaviour
{
    public GameObject enemyMainBody;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        enemyMainBody.GetComponent<BearEnemyController>().Chase(other.gameObject);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyMainBody.GetComponent<BearEnemyController>().Chase(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyMainBody.GetComponent<BearEnemyController>().Patrol();
        }
    }
}
