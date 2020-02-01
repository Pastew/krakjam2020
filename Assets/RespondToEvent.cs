using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespondToEvent : MonoBehaviour
{
    bool _canPerformHop = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respond()
    {
        if (_canPerformHop)
        {
            Debug.Log("Call beam to move.");
        }
    }
}
