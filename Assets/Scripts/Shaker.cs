using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private float _dur = 0.5f;

    [SerializeField] private float _posPow = 2;
    [SerializeField] private int _posVib = 10;

    [SerializeField] private float _rotPow = 2;
    [SerializeField] private int _rotVib = 10;

    [SerializeField] private float _scalePow = 2;
    [SerializeField] private int _scaleVib = 10;

    public void Shake()
    {
        transform.DOShakePosition(_dur, _posPow, _posVib);
        transform.DOShakeRotation(_dur, _rotPow, _rotVib);
        transform.DOShakeScale(_dur, _scalePow, _scaleVib);
    }
}