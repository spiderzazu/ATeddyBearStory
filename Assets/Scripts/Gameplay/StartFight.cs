using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    public GameEvent startBossFight;
    private bool stopChecking = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !stopChecking)
        {
            startBossFight.Rise();
            //Se desactive a si mismo este componente
            stopChecking = true;
        }
            
    }

    public void ReEnterBossFight()
    {
        stopChecking = false;
    }
}
