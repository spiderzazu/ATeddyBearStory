using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas, aboutCanvas, slotCanvas;
    public SelectedSave saveManager;

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
        saveManager.selection = file;
    }

    public void SetNewGame(bool set)
    {
        saveManager.newGame = set;
    }
}
