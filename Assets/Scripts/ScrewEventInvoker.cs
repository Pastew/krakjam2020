﻿using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    // This class is so stupid... But it's too late.
    public class ScrewEventInvoker : MonoBehaviour
    {
        private UnityEvent _screwBreakEvent;
        private UnityEvent _screwNextUnfinishedEvent;
        private UnityEvent _screwOKUnfinishedEvent;

        private void Awake()
        {
            _screwBreakEvent = new UnityEvent();
            _screwNextUnfinishedEvent = new UnityEvent();
            _screwOKUnfinishedEvent = new UnityEvent();
        }

        // Invokes
        public void InvokeScrewBreak()
        {
            _screwBreakEvent.Invoke();
        }

        public void InvokeUnfinishedEvent()
        {
            _screwNextUnfinishedEvent.Invoke();
        }
        
        public void InvokeScrewOkEvent()
        {
            _screwOKUnfinishedEvent.Invoke();
        }

        // Listeners
        public void AddScrewBreakListener(UnityAction a)
        {
            _screwBreakEvent.AddListener(a);
        }

        public void AddScrewUnfinishedListener(UnityAction a)
        {
            _screwNextUnfinishedEvent.AddListener(a);
        }
        
        public void AddScrewOkListener(UnityAction a)
        {
            _screwOKUnfinishedEvent.AddListener(a);
        }
    }
}