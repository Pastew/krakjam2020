using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
This script is used to read all the data coming from the device. For instance,
If arduino send ->
								{"1",
								"2",
								"3",}
readQueue() will return ->
								"1", for the first call
								"2", for the second call
								"3", for the thirst call

This is the perfect script for integration that need to avoid data loose.
If you need speed and low latency take a look to wrmhlReadLatest.
*/

public class Potentiometer : MonoBehaviour
{
    public static int Value = 0;
    public static int MaxVal = 1000;
    public static int MinVal = 0;
    
    public bool noArduino = false;

    wrmhl myDevice = new wrmhl(); // wrmhl is the bridge beetwen your computer and hardware.

    [Tooltip("SerialPort of your device.")]
    public string portName = "COM5";

    [Tooltip("Baudrate")] public int baudRate = 250000;

    [Tooltip("Timeout")] public int ReadTimeout = 20;

    [Tooltip("QueueLenght")] public int QueueLenght = 1;
    
    [Header("Smoothing (average of last x reads) - optional")]
    [SerializeField] private bool _smooth = false;
    [SerializeField] private int _smoothLastValuesCapacity = 10;
    private Queue<int> _lastValuesRead;
    
    private static Potentiometer _instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null) {
            _instance = this;
        } else {
            DestroyObject(gameObject);
        }
    }

    void Start()
    {
        if (noArduino)
            return;
    
        Setup();
        _lastValuesRead = new Queue<int>();
        for(int i = 0 ; i < _smoothLastValuesCapacity; i++)
            _lastValuesRead.Enqueue(50);
    }

    private void Setup()
    {
        myDevice.set(portName, baudRate, ReadTimeout,
            QueueLenght); // This method set the communication with the following vars;
                            //          Serial Port, Baud Rates, Read Timeout and QueueLenght.
        myDevice.connect(); // This method open the Serial communication with the vars previously given.
    }

    // Update is called once per frame
    void Update()
    {
        if (noArduino)
            return;

        string readQueue = myDevice.readQueue();
        Value = readQueue != null ? int.Parse(readQueue) : Value;

        if (_smooth)
        {
            while(_lastValuesRead.Count > _smoothLastValuesCapacity)
                _lastValuesRead.Dequeue();
            
            _lastValuesRead.Enqueue(Value);
            Value = (int)_lastValuesRead.Average();
        }
        //print(readQueue); // myDevice.read() return the data coming from the device using thread.
    }
    
    void OnApplicationQuit()
    {
        if (noArduino)
            return;
        // close the Thread and Serial Port
        myDevice.close();
    }
}