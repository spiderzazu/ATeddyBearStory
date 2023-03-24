using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    public GameObject[] savePoints;
    public GameObject player;
    public SelectedSave selectedSave;
    public GameEvent saveEvent;
    private float timeToSave;
    private bool alreadySaved;

    private PlayerInfo playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        alreadySaved = false;
        timeToSave = 0.0f;
        playerInfo = selectedSave.saveFiles[selectedSave.selection];
        player.transform.position = savePoints[playerInfo.savePoint].transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeToSave += Time.deltaTime;
            if (timeToSave >= 3.0f && !alreadySaved)
            {
                alreadySaved = true;
                playerInfo.currentLifePoints = playerInfo.totalLifePoints;
                playerInfo.currentAbilityPoints = playerInfo.totalAbilityPoints;
                saveEvent.Rise();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timeToSave = 0.0f;
        alreadySaved = false;
    }
}
