using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinVeggie : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;
    void Update()
    {
        transform.Rotate(rotationAxis, Time.deltaTime * 360);
    }
}
