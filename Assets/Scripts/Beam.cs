using System;
using DG.Tweening;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private float _moveX = 2f;
    [SerializeField] private AnimationCurve _moveAnimCurve;

    public void Move()
    {
        transform.DOMoveX(transform.position.x - _moveX, _duration).SetEase(_moveAnimCurve);
    }
}