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
            go.GetComponent<PlayerController2D>().OnDie();
        }
    }
}
