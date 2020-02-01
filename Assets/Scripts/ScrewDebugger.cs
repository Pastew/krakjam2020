using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScrewDebugger : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.S))
                FindObjectOfType<Screw>().Activate();
        }
    }
}