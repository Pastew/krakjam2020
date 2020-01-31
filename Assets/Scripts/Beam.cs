using DG.Tweening;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float _delayLeft = 0;

    [SerializeField] private float _delay = 1.5f;
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private float _moveX = 2f;
    [SerializeField] private AnimationCurve _moveAnimCurve;

    void Update()
    {
        _delayLeft -= Time.deltaTime;
        
        if (_delayLeft <= 0)
        {
            Move();
            _delayLeft = _delay;
        }
    }
    private void Move()
    {
        transform.DOMoveX(transform.position.x - _moveX, _duration).SetEase(_moveAnimCurve);
    }
}