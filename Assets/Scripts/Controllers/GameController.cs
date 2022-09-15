using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameOverMenu gameOverMenu;
    private IngameMenu ingameMenu;
    private ScoreComponent scoreComponent;
    private LevelController levelController;
    private int timePlayed = 0;//in seconds
    private int maxTimeLevel;
    private int currentLevel = 1;
    private bool gameOver = false;

    public delegate void OnStart(int level);
    public OnStart onStart;

    public delegate void OnTimeUpdate(int timeInSeconds);
    public OnTimeUpdate onTimeUpdate;

    private Coroutine timer;
    private WaitForSeconds waitTimer = new WaitForSeconds(1f);

    private void Awake()
    {
        scoreComponent = GetComponent<ScoreComponent>();
        levelController = GetComponent<LevelController>();
        gameOverMenu = GameObject.Find("GameOverMenu").GetComponent<GameOverMenu>(); 
        ingameMenu = GameObject.Find("IngameMenu").GetComponent<IngameMenu>();
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        onStart?.Invoke(currentLevel);
        if (timer != null)
            StopCoroutine(timer);

        timer = StartCoroutine(Timer());
        gameOver = false;
        maxTimeLevel = levelController.GetMaxTimeLevel();
        onTimeUpdate?.Invoke(maxTimeLevel);
    }

    public void Restart()
    {
        scoreComponent.Score = 0;
        timePlayed = 0;
        PauseGame(false);
        gameOverMenu.Hide();
        StartGame();
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;

        if (pause)
        {
            if (timer != null)
                StopCoroutine(timer);
        }
        else if(timer == null)
        {
            timer = StartCoroutine(Timer());
        }
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;
        SaveData("GameOver");
        gameOverMenu.Show("GameOver");
        EndGame();
    }

    public void LevelCompleted()
    {
        if (gameOver)
            return;

        gameOver = true;
        SaveData("LevelCompleted");
        gameOverMenu.Show("LevelCompleted");
        EndGame();
    }

    public void OutOfTime()
    {
        if (gameOver)
            return;

        gameOver = true;
        SaveData("OutOfTime");
        gameOverMenu.Show("OutOfTime");
        EndGame();
    }

    private void EndGame()
    {
        PauseGame(true);
        if(timer != null)
            StopCoroutine(timer);
    }

    public void ToggleIngameMenu()
    {
        if(!gameOver)
            ingameMenu.ToggleInGameMenu();
    }

    public void SaveData(string end)
    {
        GameData gameData = new GameData(Application.productName, timePlayed, scoreComponent.Score, DateTime.Now, end);
        string data = JsonUtility.ToJson(gameData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/GameData.json", data);
    }

    private IEnumerator Timer()
    {
        yield return waitTimer;
        timePlayed++;
        onTimeUpdate?.Invoke(maxTimeLevel-timePlayed);

        if (maxTimeLevel - timePlayed > 0)
        {
            timer = StartCoroutine(Timer());
        }
        else
        {
            OutOfTime();
        }
    }
}
