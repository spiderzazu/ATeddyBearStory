using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatroller : MonoBehaviour
{
    public GameObject mainBody;
    private LizardBoss lizard;
    private string nombre;

    private void Start()
    {
        lizard = mainBody.GetComponent<LizardBoss>();
        nombre = this.gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LizardBoss"))
        {
            switch (nombre)
            {
                case "Area1":
                    lizard.LizardChecker(0);
                    break;
                case "Area2":
                    lizard.LizardChecker(1);
                    break;
                case "Area3":
                    lizard.LizardChecker(2);
                    break;
                case "Area4":
                    lizard.LizardChecker(3);
                    break;
                case "Area5":
                    lizard.LizardChecker(4);
                    break;
                case "Area6":
                    lizard.LizardChecker(5);
                    break;

            }
        }
        
        
    }
}
