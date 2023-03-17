using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject hudPanel, pausePanel, settingPanel, gameOverPanel;
    /*public TextMeshProUGUI bananasText;
    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI currentLvl;
    */

    public SelectedSave saveData;
    private PlayerInfo playerData;

    public void Start()
    {
        playerData = saveData.saveFiles[saveData.selection];
        ShowHUD();
    }

    public void CleanHUD()
    {
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        CleanHUD();
        gameOverPanel.SetActive(true);
    }

    public void ShowHUD()
    {
        CleanHUD();
        hudPanel.SetActive(true);
    }

    public void ShowSetting()
    {
        CleanHUD();
        settingPanel.SetActive(true);
    }

    public void ShowPause()
    {
        CleanHUD();
        pausePanel.SetActive(true);
    }

    public void DrawStats()
    {
        /*
         bananasText.text = dkData.items.ToString();
        lifesText.text = dkData.lifes.ToString();
        currentLvl.text = "W-" + dkData.currentLvl; 
         */
    }



}
