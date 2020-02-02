using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Janusz : MonoBehaviour
{
    private Vector3 _januszPosition;
    private Image _image;
    private ScrewEventInvoker _eventInvoker;

    private int _anyEventOccuredCounter = 0;
    private bool _alreadyWalkedIn = false;

    [Header("Before Walk In")] [SerializeField]
    private int _eventsNeededToInvokeJanuszWalkIn = 3;
    [SerializeField] private int _eventsNeededToFirstImageChange = 5;

    [Header("Reactions")] [SerializeField] [Range(0, 100)]
    private int chanceToReact = 70;

    [Header("Happy Janusz Anim")] [SerializeField]
    private float _happyDuration = 2;
    [SerializeField] private float _happyPower = 2;
    [SerializeField] private AnimationCurve _happyCurve;

    [SerializeField] private Sprite _normalJanuszFace;
    [SerializeField] private List<Sprite> unfinishedScrewJanuszFaces;
    [SerializeField] private List<Sprite> okScrewJanuszFaces;
    [SerializeField] private List<Sprite> breakScrewJanuszFaces;

    [SerializeField] private List<AudioClip> _okClips;
    [SerializeField] private List<AudioClip> _unfinishedClips;
    [SerializeField] private List<AudioClip> _breakClips;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _januszPosition = transform.position;
        transform.position = _januszPosition + Vector3.right * 1600;

        _eventInvoker = FindObjectOfType<ScrewEventInvoker>();
        _eventInvoker.AddScrewBreakListener(OnScrewBreak);
        _eventInvoker.AddScrewOkListener(OnScrewOK);
        _eventInvoker.AddScrewUnfinishedListener(OnScrewUnfinished);

        _image = GetComponent<Image>();
    }

    // Janusz reactions to events
    public void OnScrewOK()
    {
        OnAnyEventOccured();
        if (!_alreadyWalkedIn || !ShouldReact())
            return;

        _image.sprite = GetNextElement(okScrewJanuszFaces);
        HappyJanuszAnimation();
        PlayRandomSound(_okClips);
    }
    
    public void OnScrewUnfinished()
    {
        OnAnyEventOccured();
        if (!_alreadyWalkedIn || !ShouldReact())
            return;
        
        _image.sprite = GetNextElement(unfinishedScrewJanuszFaces);
        PlayRandomSound(_unfinishedClips);
    }

    public void OnScrewBreak()
    {
        OnAnyEventOccured();
        if (!_alreadyWalkedIn || !ShouldReact())
            return;
        
        _image.sprite = GetNextElement(breakScrewJanuszFaces);
        PlayRandomSound(_breakClips);
        GetComponent<Shaker>().Shake();
    }

    private void OnAnyEventOccured()
    {
        _anyEventOccuredCounter++;
        if (_anyEventOccuredCounter >= _eventsNeededToInvokeJanuszWalkIn && !_alreadyWalkedIn)
            WalkIn();

        if(_anyEventOccuredCounter >= _eventsNeededToFirstImageChange)
            _image.sprite = _normalJanuszFace;
    }

    // Other
    private void WalkIn()
    {
        transform.DOMove(_januszPosition, 3f).OnComplete(() => _alreadyWalkedIn = true);
        DOVirtual.DelayedCall(2, () => PlayRandomSound(_okClips));
    }

    private void HappyJanuszAnimation()
    {
        transform.DOMoveY(transform.position.y + _happyPower, _happyDuration).SetEase(_happyCurve);
    }

    private bool ShouldReact()
    {
        if (_anyEventOccuredCounter <= _eventsNeededToFirstImageChange)
            return false;
        
        return Random.Range(0, 100) > (100 - chanceToReact);
    }

    private void PlayRandomSound(List<AudioClip> clips)
    {
        _audioSource.clip = GetNextElement(clips);
        _audioSource.Play();
    }
    
    private T GetNextElement<T>(List<T> someList)
    {
        T el = someList[0];
        someList.RemoveAt(0);
        someList.Add(el);
        return el;
    }
}