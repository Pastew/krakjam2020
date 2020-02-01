using UnityEngine;
using Random = System.Random;

public class Screw : MonoBehaviour
{
    [SerializeField] private ScrewCanvas _screwCanvas;
    
    public int Level { get; set; }
    private bool active = false;

    private int _previousPotValue;

    private void OnEnable()
    {
        _previousPotValue = 100;
        Level = new Random().Next(0, 80);
    }

    void Update()
    {
        int diff = Potentiometer.Value - _previousPotValue;
        _previousPotValue = Potentiometer.Value;
        
        if (diff > 0)
            Level += diff;
    }

    public void Activate()
    {
        active = true;
        _screwCanvas.gameObject.SetActive(true);
    }
}
