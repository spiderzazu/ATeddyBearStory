using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject hudPanel, pausePanel, settingPanel, gameOverPanel, victoryPanel, savePanel;
    public Image[] vidas;
    public Image[] energia;
    public Image[] energiaDesbloqueable;


    public SelectedSave saveData;
    private PlayerInfo playerData;

    public void Start()
    {
        
        playerData = saveData.saveFiles[saveData.selection];
        for (int i = 0; i < playerData.currentLifePoints; i++)
            vidas[i].gameObject.SetActive(true);
        ShowHUD();
        DrawStats();
    }

    public void CleanHUD()
    {
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        savePanel.SetActive(false);
    }

    public void ShowSave()
    {
        CleanHUD();
        savePanel.SetActive(true);
    }

    public void ShowVictory()
    {
        CleanHUD();
        victoryPanel.SetActive(true);
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

    public void SaveData(bool save)
    {
        if (save)
        {
            //Guardar en documento bla bla
            playerData.savePoint = 1;

            
        }
        else
        {
            return;
        }   
    }

    public void DrawLeafs()
    {
        int ajuste = 5 - playerData.totalAbilityPoints;
        int energy = playerData.currentAbilityPoints;

        if (ajuste == 1)
            energiaDesbloqueable[0].gameObject.SetActive(true);
        else if (ajuste == 0)
        {
            energiaDesbloqueable[0].gameObject.SetActive(true);
            energiaDesbloqueable[1].gameObject.SetActive(true);
        }
        
        for (int i = 0; i < energia.Length; i++)
        {
            if (i + 1 <= energy)
                energia[i].gameObject.SetActive(true);
            else
                energia[i].gameObject.SetActive(false);
        }
    }

    public void DrawStats()
    {
        int life = playerData.currentLifePoints;
        int energy = playerData.currentAbilityPoints;

        for (int i = 0; i < vidas.Length; i++)
        {
            if (i + 1 <= life)
                vidas[i].gameObject.SetActive(true);
            else
                vidas[i].gameObject.SetActive(false);
        }
        DrawLeafs();
    }



}
