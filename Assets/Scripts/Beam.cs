using System;
using DG.Tweening;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private int _stepsRightOnStart = 3;
    [SerializeField] private float _moveX = 2f;
    [SerializeField] private AnimationCurve _moveAnimCurve;
    [SerializeField] private GameObject _screwPrefab;
    [SerializeField] private int _screwsNumber = 150;
    
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        transform.Translate(Vector3.right * _stepsRightOnStart * _moveX);
        
        for(int i = 1 ; i <= _screwsNumber; i++)
            Instantiate(_screwPrefab, transform.position + Vector3.right * (i * _moveX), Quaternion.identity, transform);
    }

    public void Move()
    {
        transform.DOMoveX(transform.position.x - _moveX, _duration).SetEase(_moveAnimCurve);
        _audioSource.Play();
    }
}