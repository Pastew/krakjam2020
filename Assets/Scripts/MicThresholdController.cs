using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicThresholdController : MonoBehaviour
{
    private Slider _slider;
    private MicScript _micScript;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _micScript = FindObjectOfType<MicScript>();
    }

    void Update()
    {
        if (_micScript._calibrationPeriodOn)
            _slider.value = _micScript.ShoutThreshold;
        else
            _micScript.ShoutThreshold = _slider.value;
    }
}