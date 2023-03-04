using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafUpgrade : MonoBehaviour
{
    public GameObject particleGetPrefab;
    public GameEvent leafUpgradeEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            
            //Particles (Destruir o reusarlas?)
            Destroy(Instantiate(particleGetPrefab, other.transform.position, Quaternion.identity), 1.5f);
            leafUpgradeEvent.Rise();
            //Se podria reutilizar
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public void ActivateLeaf()
    {
        this.gameObject.SetActive(true);
    }
}
