using UnityEngine;
using UnityEngine.Events;

public class OnKeyUpEvent : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    [SerializeField] private UnityEvent _event;

    void Update()
    {
        if (Input.GetKeyUp(_key))
            _event.Invoke();
    }
}
