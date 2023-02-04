using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DestroyAfterTime))]
public class Throwable : MonoBehaviour
{
   
    public GameObject owner;

    public void SetOwer(GameObject go)
    {
        owner = go;
    }
}
