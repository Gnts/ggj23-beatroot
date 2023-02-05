using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
    MENU,
    IN_PROGRESS,
    END
}
public class Game : MonoBehaviour
{
    public GameState state;
    public const int MaxTime = 60;
    public float time;
    public TextMeshProUGUI ui_timer;
    public GameObject timerGo;
    public GameObject game_end;

    public Button buttonPlayAgain, buttonExit;

    public AudioSource audioSource;
    private bool gameEnded;
    public TextMeshProUGUI winner_text;

    void Start()
    {
        time = MaxTime;

        buttonPlayAgain.onClick.AddListener(RestartLevel);
        buttonExit.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        time -= Time.deltaTime;
        time = Math.Clamp(time, 0, MaxTime);
        
        ui_timer.text = ((int) time).ToString(CultureInfo.InvariantCulture);
        if (((int)time) == 0 && !gameEnded)
        {
            gameEnded = true;
            GameEnd();
        }
        
        //update audio pitch
        if (audioSource)
        {
            if ((int) time < 10) audioSource.pitch = 1.5f;
            else if ((int) time < 20) audioSource.pitch = 1.2f;
        }
    }

    void GameEnd()
    {
        timerGo.SetActive(false);
        game_end.SetActive(true);

        var players = FindObjectsOfType<PlayerController2D>();
        int winnerIndex = 0;
        int winnerDeathCount = 999;
        foreach (var player in players)
        {
            if (player.deathCounter >= winnerDeathCount) continue;
            winnerIndex = player.playerIndex;
            winnerDeathCount = player.deathCounter;
        }

        winner_text.text += $"\n{PlayerController2D.indexToColor[winnerIndex]}";
        winner_text.color = Color.magenta;
    }
    
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
