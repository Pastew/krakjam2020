using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x,
                                           (MicTestScript._bandBuffer[_band] * _scaleMultiplier) + _startScale,
                                           transform.localScale.z);
    }

    public void Hop()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y +2, transform.localPosition.z);
    }
}
