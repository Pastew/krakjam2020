using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float _startingTimeForOneScrew = 10f;
    [SerializeField] float _timeDecrease = 0.5f;
    [SerializeField] private float _minTimeForOneScrew = 2f;
   
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _watchPointer;

    private float _timeForOneScrew;
    private float _timeLeft = 0;

    void Start()
    {
        _timeForOneScrew = _startingTimeForOneScrew;
        Reset();
    }

    public void Reset()
    {
        _timeLeft = _timeForOneScrew;
        _timeForOneScrew -= _timeDecrease;
       
        if (_timeForOneScrew < _minTimeForOneScrew)
            _timeForOneScrew = _minTimeForOneScrew;
    }

    void Update()
    {
        _timeLeft -= Time.deltaTime;
        _slider.value = _timeLeft / _timeForOneScrew;

        if (_timeLeft < 0)
        {
            Reset();
            FindObjectOfType<MicScript>().InvokeShoutEvent();
        }

        float z = Helpers.Remap(_timeLeft, _timeForOneScrew, 0, -1, -364);
        _watchPointer.transform.rotation = Quaternion.Euler(0, 0, z);
    }
}