using UnityEngine;
using UnityEngine.UI;

public class NoiseMeter : MonoBehaviour
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
        _slider.value = _micScript.CurrVolume;
    }
}