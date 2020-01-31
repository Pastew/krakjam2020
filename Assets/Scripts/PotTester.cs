using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTester : MonoBehaviour
{
    void Update()
    {
        float x = Remap(Potentiometer.Value, 0, 100, 0, Screen.width);
        transform.position = new Vector3(x, transform.position.y, 0);   
    }
    
    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
