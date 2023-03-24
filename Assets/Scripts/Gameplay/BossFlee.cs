using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlee : MonoBehaviour
{
    public GameEvent bossFlee;
    private bool stopChecking = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !stopChecking)
        {
            bossFlee.Rise();
            //Se desactive a si mismo este componente
            stopChecking = true;
        }

    }

    public void ReEnterBossFight()
    {
        stopChecking = false;
    }
}
