﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCubes = new GameObject[512];
    public float _maxScale; 
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 512; i++)
        //{
        //    GameObject _sampleCubeInstance = (GameObject) Instantiate (_sampleCubePrefab);
        //    _sampleCubeInstance.transform.position = this.transform.position;
        //    _sampleCubeInstance.transform.parent = this.transform;
        //    _sampleCubeInstance.name = "samplecube " + i;
        //    this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
        //    _sampleCubeInstance.transform.position = Vector3.forward * 100;
        //    _sampleCubes[i] = _sampleCubeInstance;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < 512; i++)
        //{
        //    if (_sampleCubes != null)
        //    {
        //        _sampleCubes[i].transform.localScale = new Vector3(10, (MicTestScript._samples[i] * _maxScale) + 2, 10);
        //    }
        //}
    }
}
