using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int grosze = 0;
    [SerializeField] private int scoreUpValue = 5;
    [SerializeField] private Text _text;

    [SerializeField] private GameObject _wallet;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _coinStartPos;
    [SerializeField] private float _coinDuration = 0.7f;
    [SerializeField] private AnimationCurve _coinScaleEase;
    [SerializeField] private AnimationCurve _coinMoveEase;

    void Start()
    {
       FindObjectOfType<ScrewEventInvoker>().AddScrewOkListener(ScoreUp);
    }

    public void ScoreUp()
    {
        grosze += scoreUpValue;
        float zlotowki = grosze / 100;
        float groszowki = grosze % 100;

        string scoreText = String.Format("{0:0}", zlotowki) + "." + String.Format("{0:00}", groszowki) + "zł";
        _text.text = scoreText;

        GameObject coinGO = Instantiate(_coinPrefab, _coinStartPos.transform.position, Quaternion.identity, transform);
        coinGO.transform.DOScale(2f, _coinDuration * 0.9f).SetEase(_coinScaleEase);
        coinGO.transform.DOMove(_wallet.transform.position, _coinDuration).SetEase(_coinMoveEase).OnComplete(() =>
        {
            Destroy(coinGO);
            _wallet.GetComponent<Shaker>().Shake();
        });
    }
}
