using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMaster : MonoBehaviour
{
    public GameObject leafPrefab;
    public Transform[] spawnPoints;
    public GameObject[] activeLeafs;

    public void SpawnLeafs()
    {
        for(int i = 0; i<spawnPoints.Length; i++)
        {
            activeLeafs[i] = Instantiate(leafPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }

    public void ReActivateLeafs()
    {
        for(int i = 0; i<activeLeafs.Length; i++)
        {
            if (!activeLeafs[i].gameObject.activeSelf)
                activeLeafs[i].SetActive(true);
        }
    }
}
