using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] private float _minRotationZ = -18f;
    [SerializeField] private float _maxRotationZ = -85;

    [Header("Move animation")] [SerializeField]
    private float _moveAnimDuration = 0.5f;

    [SerializeField] private AnimationCurve _moveOutCurve;
    [SerializeField] private AnimationCurve _scaleOutCurve;

    [SerializeField] private AnimationCurve _scaleInCurve;
    [SerializeField] private AnimationCurve _moveInCurve;

    [SerializeField] private float _moveDownShift = 3f;

    private Vector3 _startingScale;

    private float _previousZRotation = 0;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _screwAudioCLip;
    [SerializeField] private AudioClip _unscrewAudioClip;
    [SerializeField] private int _audioChangeTheshold = 10;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _startingScale = transform.localScale;
    }

    void Update()
    {
        float zRotation = Helpers.Remap(Potentiometer.Value,
            Potentiometer.MinVal, Potentiometer.MaxVal,
            _minRotationZ, _maxRotationZ);

        if (Math.Abs(_previousZRotation - zRotation) > _audioChangeTheshold)
        {
            ChangeAudio(zRotation);
        }


        transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }

    private void ChangeAudio(float zRotation)
    {
        if (_previousZRotation > zRotation)
        {
            _audioSource.clip = _screwAudioCLip;
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else if (_previousZRotation < zRotation)
        {
            _audioSource.clip = _unscrewAudioClip;
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else if (Math.Abs(_previousZRotation - zRotation) > _audioChangeTheshold)
        {
            _audioSource.Pause();
        }

        _previousZRotation = zRotation;
    }

    public void NextScrew()
    {
        Vector3 targetPos = transform.position;
        Vector3 offsetPosition = transform.position - Vector3.down * _moveDownShift;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(offsetPosition, _moveAnimDuration / 2).SetEase(_moveOutCurve));
        s.Join(transform.DOScale(1.1f, _moveAnimDuration).SetEase(_scaleOutCurve));

        s.AppendInterval(0.15f);
        s.Append(transform.DOMove(targetPos, _moveAnimDuration / 2).SetEase(_moveInCurve));
        s.Join(transform.DOScale(_startingScale, _moveAnimDuration).SetEase(_scaleInCurve));

        s.Play();
    }
}