using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScrewCanvas : MonoBehaviour
{
    [SerializeField] private Screw _screw;
    [SerializeField] private Text _levelText;
    [SerializeField] private Slider _levelSlider;
    private GameValues _gameValues;

    [SerializeField] private float _animDuration = 0.5f;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private AnimationCurve _scaleCurve;

    void Awake()
    {
        _gameValues = FindObjectOfType<GameValues>();
    }

    private void OnEnable()
    {
        Vector3 targetPos = transform.position;
        Vector3 targetScale = transform.localScale;

        Vector3 startPos = _screw.transform.position;
        Vector3 startScale = Vector3.zero;

        transform.position = startPos;
        transform.localScale = startScale;
        
        transform.DOMove(targetPos, _animDuration).SetEase(_moveCurve);
        transform.DOScale(targetScale, _animDuration).SetEase(_scaleCurve);
    }

    void Update()
    {
        _levelSlider.value = _screw.Level;
        _levelText.text = $"{_screw.Level}%";
    }
}
