using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Screw : MonoBehaviour
{
    [SerializeField] private ScrewCanvas _screwCanvas;
    [SerializeField] private GameObject _view;

    private bool active = false;

    private float _level;
    public int Level => (int) _level;

    [SerializeField] private float _multiplier = 0.5f;
    private int _previousPotValue;

    [SerializeField] private float _visualRotateMultiplier = 1f;
    private int _diff;
    private GameValues _gameValues;

    private void OnEnable()
    {
        Random.seed = System.DateTime.Now.Millisecond;

        _previousPotValue = (int) Potentiometer.MaxVal;
        _level = Random.Range(Potentiometer.MinVal, Potentiometer.MaxVal);
    }

    private void Start()
    {
        _gameValues = FindObjectOfType<GameValues>();
    }

    void Update()
    {
        if (!active)
            return;

        CalculateNewLevel();
        RotateView();
        
        if (Level > _gameValues._breakLevel)
            BreakScrew();
    }

    private void BreakScrew()
    {
        active = false;
        transform.DOShakeScale(0.5f, 0.7f, 50).OnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Enter");
        if(other.GetComponent<ScrewActivator>())
            Activate();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        print("Exit");

        if(other.GetComponent<ScrewActivator>())
            Deactivate();
    }

    private void CalculateNewLevel()
    {
        _diff = Potentiometer.Value - _previousPotValue;
        _previousPotValue = Potentiometer.Value;

        if (_diff > 0)
            _level += _diff * _multiplier;
    }

    private void RotateView()
    {
        if (_diff > 0)
            _view.transform.Rotate(Vector3.forward, -_visualRotateMultiplier * _diff);
    }

    public void Activate()
    {
        active = true;
        _screwCanvas.gameObject.SetActive(true);
    }
    
    public void Deactivate()
    {
        active = false;
        _screwCanvas.Deactivate();
    }
}