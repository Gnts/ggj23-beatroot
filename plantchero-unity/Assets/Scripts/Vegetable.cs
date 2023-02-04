using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    public int currentHealth;
    int maxHelath = 10;

    public float healthInterval = 2.0f; 
    float nextTime = 0;

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
        currentHealth = currentHealth - amount;
        if (currentHealth < 1)
        {
            return gameObject;
        }
        return null;
    }
}
