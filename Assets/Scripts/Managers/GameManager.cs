using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState currentState;
    public GameEvent OnStartEvent;
    void Start()
    {
        currentState = GameState.ON_START;
        Time.timeScale = 1;
        EvaluateState();
    }
    public void EvaluateState()
    {
        switch (currentState)
        {
            case GameState.ON_START:
                OnStartEvent.Rise();
                break;
        }
    }

    public void PauseGame()
    {
        currentState = GameState.PAUSE;
        Time.timeScale = 0;
        EvaluateState();
    }
    public void ContinueGame()
    {
        currentState = GameState.PLAYING;
        Time.timeScale = 1;
        EvaluateState();
    }
    public void ExitGame()
    {
        currentState = GameState.GAME_OVER;
        EvaluateState();
        Application.Quit();
    }
    public void MainMenu()
    {
        currentState = GameState.GAME_OVER;
        SceneManager.LoadScene("MainMenu");
    }

}

public enum GameState
{
    LOADING,
    ON_START,
    PLAYING,
    GAME_OVER,
    PAUSE
}
