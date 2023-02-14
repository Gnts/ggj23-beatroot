using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public static Game singleton;
    public GameState state;
    public const int MaxTime = 100;
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

    public List<PlayerController2D> players = new List<PlayerController2D>();
    public Transform score_canvas;
    public GameObject score_fab;
    
    void Start()
    {
        singleton = this;
        time = MaxTime;

        buttonPlayAgain.onClick.AddListener(RestartLevel);
        buttonExit.onClick.AddListener(ExitGame);

        ChangeState(GameState.LOBBY);
    }

    bool isGamePadStartDown()
    {
        if (Gamepad.current == null) return false;
        return Gamepad.current.startButton.IsPressed();
    }
    void Update()
    {
        switch(state) 
        {
            case GameState.LOBBY:
                if (Input.GetKey(KeyCode.Return) || isGamePadStartDown())
                {
                    ChangeState(GameState.COUNTDOWN);
                }
                break;
            case GameState.COUNTDOWN:
                //Canvas obj takes care of state change
                break;
            case GameState.PLAYING:
                UpdatePlaying();

                break;
            case GameState.ENDSCREEN:
                if (Input.GetKey(KeyCode.Return) || (Gamepad.current == null ? false : Gamepad.current.startButton.IsPressed()))
                {
                    ChangeState(GameState.LOBBY);
                    RestartLevel();
                }
                break;
        }
    }

    public void UpdateScores()
    {
        int winnerIndex = 0;
        int winnerDeathCount = 999;
        foreach (var player in players)
        {
            if (player.deathCounter >= winnerDeathCount) continue;
            winnerIndex = player.playerIndex;
            winnerDeathCount = player.deathCounter;
        }

        foreach (var player in players)
        {
            var scoreCard = player.score_card.GetComponent<IUpdateScoreCard>();
            scoreCard.SetCrown(player.playerIndex == winnerIndex);
            scoreCard.SetScore(player.deathCounter);
        }
        
    }

    void UpdatePlaying()
    {

        
        time -= Time.deltaTime;
        time = Math.Clamp(time, 0, MaxTime);

        //update audio pitch
        if (audioSource)
        {
            if ((int) time < 10) audioSource.pitch = 1.5f;
            else if ((int) time < 20) audioSource.pitch = 1.2f;
        }
        
        ui_timer.text = ((int) time).ToString(CultureInfo.InvariantCulture);
        if (((int)time) == 0 && !gameEnded)
        {
			gameEnded = true;
            ChangeState(GameState.ENDSCREEN);    
        }
    }

public void ChangeState(GameState newState)
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
        audioSource.time = 7;
        audioSource.pitch = 1.3f;

        int winnerIndex = 0;
        int winnerDeathCount = 999;
        foreach (var player in players)
        {
            if (player.deathCounter >= winnerDeathCount) continue;
            winnerIndex = player.playerIndex;
            winnerDeathCount = player.deathCounter;
        }

        winner_text.text += $"\n{PlayerController2D.indexToColor[winnerIndex]}";
        winner_text.color = PlayerController2D.indexToColorName[winnerIndex];
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

    public bool IsMovementAllowed()
    {
        if ((state == GameState.PLAYING) || (state == GameState.ENDSCREEN) )  return true;

        return false;
    }
    
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
