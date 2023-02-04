using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    public static int count = 0;
    public int currentHealth;
    public ThrowableVeggie type;
    int maxHelath = 3;
    public int potIndex;

    public float healthInterval = 2.0f; 
    float nextTime = 0;

    public void OnEnable()
    {
        count++;
    }

    private void OnDisable()
    {
        count--;
        IPlantVeggies.occupied.Remove(potIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 1;
    }

    private void Update() 
    {
        if (Time.time >= nextTime) 
        {
            if (currentHealth < maxHelath)
            {
                currentHealth += 1;
            }
            nextTime += healthInterval; 
         }    
    }

    public GameObject DigIt(int amount)
    {
        transform.localScale = new Vector3(transform.lossyScale.x * 0.9f, transform.lossyScale.y * 1.1f, transform.lossyScale.z * 0.9f);
        currentHealth = currentHealth - amount;
        if (currentHealth < 1)
        {
            return gameObject;
        }
        return null;
    }
}
