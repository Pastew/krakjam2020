using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScrewCanvas : MonoBehaviour
{
    [SerializeField] private Screw _screw;
    [SerializeField] private Text _levelText;
    [SerializeField] private Slider _linearProgress;
    [SerializeField] private Image _radialProgress;
    private GameValues _gameValues;

    [SerializeField] private float _onEnableAnimDuration = 0.5f;
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private AnimationCurve _scaleCurve;

    private CanvasGroup _canvasGroup;
    [SerializeField] private float _onDeactivateAnimDuration = 0.5f;

    void Awake()
    {
        _gameValues = FindObjectOfType<GameValues>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        Vector3 targetPos = transform.position;
        Vector3 targetScale = transform.localScale;

        Vector3 startPos = _screw.transform.position;
        Vector3 startScale = Vector3.zero;

        transform.position = startPos;
        transform.localScale = startScale;
        _canvasGroup.alpha = 0;

        transform.DOMove(targetPos, _onEnableAnimDuration).SetEase(_moveCurve);
        transform.DOScale(targetScale, _onEnableAnimDuration).SetEase(_scaleCurve);
        _canvasGroup.DOFade(1, _onEnableAnimDuration).SetEase(_scaleCurve);
    }

    void Update()
    {
        _linearProgress.value = _screw.Level;
        double percents = 100 * _screw.Level / (double) Potentiometer.MaxVal;
        _levelText.text = $"{(int)percents}%";
        _radialProgress.fillAmount = _screw.Level / (float) Potentiometer.MaxVal;
    }

    public void Deactivate()
    {
        transform.DOMove(transform.position + Vector3.up, _onDeactivateAnimDuration).SetEase(_moveCurve);
        _canvasGroup.DOFade(0, _onDeactivateAnimDuration).SetEase(_scaleCurve);
    }
}