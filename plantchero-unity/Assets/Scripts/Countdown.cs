using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public void NotifyCountdownEnded()
    {
        Game gameController = FindObjectOfType<Game>();
        gameController.ChangeState(GameState.PLAYING);
    }
    
}
