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
        _slider.value = _micScript.ShoutThreshold;
    }

    void Update()
    {
        _micScript.ShoutThreshold = _slider.value;
    }
}