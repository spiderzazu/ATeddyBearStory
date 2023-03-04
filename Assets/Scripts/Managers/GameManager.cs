using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentState;
    public GameEvent OnStartEvent;
    void Start()
    {
        currentState = GameState.ON_START;
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
}

public enum GameState
{
    LOADING,
    ON_START,
    PLAYING,
    GAME_OVER,
    PAUSE
}
