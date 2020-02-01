using DefaultNamespace;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] private float _minRotationZ = -18f;
    [SerializeField] private float _maxRotationZ = -85;

    void Update()
    {
        float zRotation = Helpers.Remap(Potentiometer.Value,
            Potentiometer.MinVal, Potentiometer.MaxVal,
            _minRotationZ, _maxRotationZ);
        
        transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
}