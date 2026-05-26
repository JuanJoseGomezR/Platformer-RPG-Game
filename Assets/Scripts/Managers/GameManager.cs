using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    private GameState _currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SaveManager.Instance.LoadGame(0);
    }

    public void SetState(GameState newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level2");
    
    }
}
