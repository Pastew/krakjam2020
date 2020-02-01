using UnityEngine;
using Random = System.Random;

public class Screw : MonoBehaviour
{
    [SerializeField] private ScrewCanvas _screwCanvas;

    private bool active = false;
    
    private float _level;
    public int Level => (int) _level;

    [SerializeField] private float _multiplier = 0.5f;
    private int _previousPotValue;

    private void OnEnable()
    {
        _previousPotValue = 100;
        _level = new Random().Next(0, 80);
    }

    void Update()
    {
        if(!active)
            return;

        int diff = Potentiometer.Value - _previousPotValue;
        _previousPotValue = Potentiometer.Value;
        
        if (diff > 0)
            _level += diff * _multiplier;
    }

    public void Activate()
    {
        active = true;
        _screwCanvas.gameObject.SetActive(true);
    }
}
