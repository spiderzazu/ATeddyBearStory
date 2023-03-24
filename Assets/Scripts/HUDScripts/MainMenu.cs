using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas, aboutCanvas, slotCanvas;
    public SelectedSave saveManager;
    public PlayerInfo defaultSave;

    private void Start()
    {
        SetSavedData();
        mainCanvas.SetActive(true);
        aboutCanvas.SetActive(false);
        slotCanvas.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Lvl0");
    }

    public void Slots()
    {
        mainCanvas.SetActive(false);
        slotCanvas.SetActive(true);
    }

    public void About()
    {
        aboutCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void Back()
    {
        aboutCanvas.SetActive(false);
        slotCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void SetSaveFile(int file)
    {
        if (saveManager.newGame)
        {
            NewGameFile(file);
        }
        saveManager.selection = file;
    }

    public void SetNewGame(bool set)
    {
        saveManager.newGame = set;
    }

    private void SetSavedData()
    {

    }

    private void NewGameFile(int file)
    {
        PlayerInfo tempFile = saveManager.saveFiles[file];
        tempFile.totalLifePoints = defaultSave.totalLifePoints;
        tempFile.currentLifePoints = defaultSave.totalLifePoints;
        tempFile.totalAbilityPoints = defaultSave.totalAbilityPoints;
        tempFile.lifePointsCollected = defaultSave.lifePointsCollected;
        tempFile.abilityLeafs = defaultSave.abilityLeafs;
        tempFile.leafFragments = defaultSave.leafFragments;

    }
}
