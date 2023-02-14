using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingRoot : MonoBehaviour
{

    private Game gameController;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<Game>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if (gameController.state == GameState.PLAYING)
        {
            animator.StopPlayback();
        }
        else
        {
            animator.StartPlayback();
        }
    }
}
