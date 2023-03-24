using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameEvent rockHitEvent;
    public SelectedSave selectedSave;
    public BearEnemyData lizardData;
    private PlayerInfo playerInfo;

    public void Start()
    {
        SetSaveData();
    }

    public void SetSaveData()
    {
        playerInfo = selectedSave.saveFiles[selectedSave.selection];
    }

    private void OnCollisionEnter(Collision collision)
    {
        rockHitEvent.Rise();
        //Debug.Log("Choque con: " + collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
