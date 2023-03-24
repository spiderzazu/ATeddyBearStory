using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPatroller : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player"))
        {
            switch (nombre)
            {
                case "Area1":
                    lizard.PlayerChecker(0);
                    break;
                case "Area2":
                    lizard.PlayerChecker(1);
                    break;
                case "Area3":
                    lizard.PlayerChecker(2);
                    break;
                case "Area4":
                    lizard.PlayerChecker(3);
                    break;
                case "Area5":
                    lizard.PlayerChecker(4);
                    break;
                case "Area6":
                    lizard.PlayerChecker(5);
                    break;

            }
        }


    }
}

