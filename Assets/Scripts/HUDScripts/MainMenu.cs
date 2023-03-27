using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas, aboutCanvas, slotCanvas, tutorialCanvas;
    public SelectedSave saveManager;
    public PlayerInfo defaultSave;
    public TextMeshProUGUI[] vidas;
    public TextMeshProUGUI[] energia;
    public TextMeshProUGUI[] fragmentos;
    public TextMeshProUGUI[] savePoints;

    private string[] archivoGuardado;
    private SaveJ tmpSaveFile = new SaveJ();
    private SaveJ save = new SaveJ();

    private void Awake()
    {
        archivoGuardado = new string[] { Application.dataPath + "/saveFile0.json", Application.dataPath + "/saveFile1.json", Application.dataPath + "/saveFile2.json" };
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(archivoGuardado[i]))
            {
                string contenido = File.ReadAllText(archivoGuardado[i]);
                tmpSaveFile = JsonUtility.FromJson<SaveJ>(contenido);
                saveManager.saveFiles[i].totalLifePoints = tmpSaveFile.life;
                saveManager.saveFiles[i].totalAbilityPoints = tmpSaveFile.ability;
                saveManager.saveFiles[i].leafFragments = tmpSaveFile.fragments;
                saveManager.saveFiles[i].savePoint = tmpSaveFile.position;
            }
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        SetSavedData();
        CleanHUD();
        mainCanvas.SetActive(true);
    }

    public void CleanHUD()
    {
        mainCanvas.SetActive(false);
        aboutCanvas.SetActive(false);
        slotCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Lvl0");
    }

    public void Slots()
    {
        CleanHUD();
        slotCanvas.SetActive(true);
    }

    public void Tutorial()
    {
        CleanHUD();
        tutorialCanvas.SetActive(true);
    }

    public void About()
    {
        CleanHUD();
        aboutCanvas.SetActive(true);
    }

    public void Back()
    {
        CleanHUD();
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
        PlayerInfo[] datos = new PlayerInfo[3] { saveManager.saveFiles[0], saveManager.saveFiles[1], saveManager.saveFiles[2] };
        for(int i = 0; i < 3; i++)
        {
            vidas[i].text = "Vidas " + datos[i].totalLifePoints.ToString();
            energia[i].text = "Energia " + datos[i].totalAbilityPoints.ToString();
            fragmentos[i].text = "Fragmentos recogidos " + datos[i].leafFragments.ToString();
            switch (datos[i].savePoint)
            {
                case 0:
                    savePoints[i].text = "Cueva de los inicios";
                    break;
                case 1:
                    savePoints[i].text = "Fuente de guardado";
                    break;
                default:
                    savePoints[i].text = "Cueva de los inicios";
                    break;
            }
        }
    }

    private void NewGameFile(int file)
    {
        //Guardar nueva partida
        SaveJ newSave = new SaveJ()
        {
            life = defaultSave.totalLifePoints,
            ability = defaultSave.totalAbilityPoints,
            fragments = defaultSave.leafFragments,
            position = defaultSave.savePoint
        };
        string cadenaJSON = JsonUtility.ToJson(newSave);
        File.WriteAllText(archivoGuardado[file], cadenaJSON);

        PlayerInfo tempFile = saveManager.saveFiles[file];
        tempFile.totalLifePoints = defaultSave.totalLifePoints;
        tempFile.currentLifePoints = defaultSave.totalLifePoints;
        tempFile.totalAbilityPoints = defaultSave.totalAbilityPoints;
        tempFile.currentAbilityPoints = 1;
        tempFile.lifePointsCollected = defaultSave.lifePointsCollected;
        tempFile.abilityLeafs = defaultSave.abilityLeafs;
        tempFile.leafFragments = defaultSave.leafFragments;
        tempFile.savePoint = defaultSave.savePoint;

    }
}
