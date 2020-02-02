using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float _timeForOneScrew = 7f;
    [SerializeField] private Slider _slider;
    
    private float _timeLeft = 0;
        
    void Start()
    {
        _timeLeft = _timeForOneScrew;
    }

    public void Reset()
    {
        _timeLeft = _timeForOneScrew;
    }

    void Update()
    {
        _timeLeft -= Time.deltaTime;
        _slider.value = _timeLeft / _timeForOneScrew;
        
        if (_timeLeft < 0)
        {
            _timeLeft = _timeForOneScrew;
            FindObjectOfType<MicScript>().InvokeShoutEvent();
        }
    }
}
