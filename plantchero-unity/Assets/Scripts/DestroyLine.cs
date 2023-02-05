using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var go = col.gameObject;
        if (go.tag.Equals("Player"))
        {
            var controller = go.GetComponent<PlayerController2D>();
            controller.deathCounter++;
            go.transform.position = new Vector3(0, 6, 0);
            controller.activeVeggie = ThrowableVeggie.NONE;
            controller.beetrootIcon.gameObject.SetActive(false);
            controller.carrotIcon.gameObject.SetActive(false);
            controller.potatoIcon.gameObject.SetActive(false);
            Game.singleton.UpdateScores();
        }
    }
}
