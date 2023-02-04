using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private SimpleMultiplayerControls input;

    void Start()
    {
        input = new SimpleMultiplayerControls();
        input.Enable();
        input.Player.Enable();
    }

    void Update()
    {
        var move_vector = input.Player.Movement.ReadValue<Vector2>();
        Debug.Log(move_vector);
    }
}