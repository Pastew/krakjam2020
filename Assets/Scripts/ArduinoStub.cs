using UnityEngine;
using UnityEngine.UI;

public class ArduinoStub : MonoBehaviour
{
    private Potentiometer _pot;
    private Slider _slider;

    void Start()
    {
        _slider = GetComponent<Slider>();
        
        _pot = FindObjectOfType<Potentiometer>();
        if(!_pot.noArduino)
            Destroy(gameObject);
    }
    
    void Update()
    {
        Potentiometer.Value = (int) _slider.value;
    }
}
