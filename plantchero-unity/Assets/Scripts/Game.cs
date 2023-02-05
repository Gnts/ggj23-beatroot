using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
    LOBBY,
    COUNTDOWN,
    PLAYING,
    ENDSCREEN
}

public class Game : MonoBehaviour
{
    public GameState state;
    public const int MaxTime = 60;
    public float time;
    public TextMeshProUGUI ui_timer;
    public GameObject canvasTitle;
    public GameObject canvasCountdown;
    public GameObject canvasWinScreen;
    public GameObject canvasTimer;

    public AudioSource audioSource;

    public AudioClip lobbyMusic; 
    public AudioClip gameMusic; 
    public AudioClip endScreenMusic; 

    public Button buttonPlayAgain, buttonExit;

    private bool gameEnded;
    public TextMeshProUGUI winner_text;
    public GameObject planter;
    
    void Start()
    {
        time = MaxTime;

        buttonPlayAgain.onClick.AddListener(RestartLevel);
        buttonExit.onClick.AddListener(ExitGame);

        ChangeState(GameState.PLAYING);
    }

    void Update()
    {
        switch(state) 
        {
            case GameState.LOBBY:
                UpdatePlaying();

                break;
            case GameState.COUNTDOWN:

                break;
            case GameState.PLAYING:

                break;
            case GameState.ENDSCREEN:

                break;
        }
    }

    void UpdatePlaying()
    {
        time -= Time.deltaTime;
        time = Math.Clamp(time, 0, MaxTime);
        
        ui_timer.text = ((int) time).ToString(CultureInfo.InvariantCulture);
        if (((int)time) == 0 && !gameEnded)
        {
			gameEnded = true;
            StartEndScreen();
			canvasTimer.SetActive(false);
            canvasWinScreen.SetActive(true);        
        }

        //update audio pitch
        if (audioSource)
        {
            if ((int) time < 10) audioSource.pitch = 1.5f;
            else if ((int) time < 20) audioSource.pitch = 1.2f;
        }
    }

void ChangeState(GameState newState)
    {
        switch(newState) 
        {
            case GameState.LOBBY:
                state = GameState.LOBBY;
                StartLobby();

                break;
            case GameState.COUNTDOWN:
                state = GameState.COUNTDOWN;
                StartCountdown();

                break;
            case GameState.PLAYING:
                state = GameState.PLAYING;
                StartPlaying();

                break;
            case GameState.ENDSCREEN:
                state = GameState.ENDSCREEN;
                StartEndScreen();

            break;
        }
    }

    void StartLobby()
    {
        ChangeUI(GameState.LOBBY);
        planter.SetActive(false);
        PlayAudio(lobbyMusic);
    }

    void StartCountdown()
    {
        ChangeUI(GameState.COUNTDOWN);
        planter.SetActive(false);
        PlayAudio(null);
    }

    void StartPlaying()
    {
        ChangeUI(GameState.PLAYING);
        planter.SetActive(true);
        PlayAudio(gameMusic);
    }

    void StartEndScreen()
    {
        ChangeUI(GameState.ENDSCREEN);
        planter.SetActive(false);
        PlayAudio(endScreenMusic);

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

    void ChangeUI(GameState newState)
    {
        canvasTitle.SetActive(false);
        canvasCountdown.SetActive(false);
        canvasWinScreen.SetActive(false);
        canvasTimer.SetActive(false);

        switch(newState) 
        {
            case GameState.LOBBY:
                canvasTitle.SetActive(true);
                break;
            case GameState.COUNTDOWN:
                canvasCountdown.SetActive(true);
                break;
            case GameState.PLAYING:
                canvasTimer.SetActive(true);
                break;
            case GameState.ENDSCREEN:
                canvasWinScreen.SetActive(true);
            break;
        }
    }

    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = 1;
        audioSource.Play();
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
