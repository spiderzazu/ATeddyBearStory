using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save Manager", menuName = "PlayerData/SaveManager")]
public class SelectedSave : ScriptableObject
{
    public int selection = 0;
    public bool newGame;

    public PlayerInfo[] saveFiles;
}
