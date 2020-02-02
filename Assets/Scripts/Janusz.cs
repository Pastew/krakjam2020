using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class Janusz : MonoBehaviour
{
    private Vector3 _januszPosition;

    private ScrewEventInvoker _eventInvoker;
    
    private int _anyEventOccuredCounter = 0;
    private bool _alreadyWalkedIn = false;
    [SerializeField] private int _eventsNeededToInvokeJanuszWalkIn = 3;
    
    [Header("Happy Janusz Anim")]
    [SerializeField] private float _happyDuration = 2;
    [SerializeField] private float _happyPower = 2;
    [SerializeField] private AnimationCurve _happyCurve;

    void Start()
    {
        _januszPosition = transform.position;
        transform.position = _januszPosition + Vector3.right * 300;

        _eventInvoker = FindObjectOfType<ScrewEventInvoker>();
        _eventInvoker.AddScrewBreakListener(OnScrewBreak);
        _eventInvoker.AddScrewOkListener(OnScrewOK);
        _eventInvoker.AddScrewUnfinishedListener(OnScrewUnfinished);
    }

    private void OnScrewOK()
    {
        OnAnyEventOccured();
        if (!_alreadyWalkedIn)
            return;

        HappyJanuszAnimation();
    }

    private void OnScrewUnfinished()
    {
        OnAnyEventOccured();
        if (!_alreadyWalkedIn)
            return;
    }

    private void OnScrewBreak()
    {
        OnAnyEventOccured();
        if (!_alreadyWalkedIn)
            return;
        
        GetComponent<Shaker>().Shake();
    }

    private void OnAnyEventOccured()
    {
        _anyEventOccuredCounter++;
        print("Any event");
        if (_anyEventOccuredCounter >= _eventsNeededToInvokeJanuszWalkIn && !_alreadyWalkedIn)
            WalkIn();
    }

    private void WalkIn()
    {
        print("WalkIn");
        transform.DOMove(_januszPosition, 3f).OnComplete(() => _alreadyWalkedIn = true);
    }

    private void HappyJanuszAnimation()
    {
        transform.DOMoveY(transform.position.y + _happyPower, _happyDuration).SetEase(_happyCurve);
    }
}