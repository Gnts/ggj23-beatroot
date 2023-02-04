using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public enum GameState {
    MENU,
    IN_PROGRESS,
    END
}
public class Game : MonoBehaviour
{
    public GameState state;
    public const int MaxTime = 2;
    public float time;
    public TextMeshProUGUI ui_timer;
    public GameObject game_end;
    
    void Start()
    {
        time = MaxTime;
    }
    void Update()
    {
        time -= Time.deltaTime;
        time = Math.Clamp(time, 0, MaxTime);
        
        ui_timer.text = ((int) time).ToString(CultureInfo.InvariantCulture);
        if(((int) time) == 0) game_end.SetActive(true);
    }
}
