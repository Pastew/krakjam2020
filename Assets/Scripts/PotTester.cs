using DefaultNamespace;
using UnityEngine;

public class PotTester : MonoBehaviour
{
    void Update()
    {
        float x = Helpers.Remap(Potentiometer.Value, Potentiometer.MinVal, Potentiometer.MaxVal, 0, Screen.width);
        transform.position = new Vector3(x, transform.position.y, 0);   
    }
}
