using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class MicScript : MonoBehaviour
{
    public UnityEvent shoutEvent;
    AudioSource _micInput;
    public AudioMixerGroup _micMixerGroup;

    string _microphoneDeviceName;

    public static float[] _samples = new float[512];

    public static float[] _frequencyBands = new float[8]; // at the moment
    public static float[] _bandBuffer = new float[8]; // at the moment
    float[] _bufferDecrease = new float[8];

    public static float[] _frequencyBandsMaxValues = new float[8];

    int _updatesCount;
    bool _canPerformHop = true;

    public float ShoutThreshold = 10;
    public float CurrVolume = 0;

    void Start()
    {
        InitArrays();
        SetupMicrophone();
        PrintMicrophoneInfo();
        StartCapturingMicrophoneInput();
    }

    // Update is called once per frame
    void Update()
    {
        FillSpectrumData();
        MakeFrequencyBands();
        BandBuffer();
        DetectShout();

        if (Input.GetKeyUp(KeyCode.E))
            shoutEvent.Invoke();
    }

    void InitArrays()
    {
        for (int i = 0; i < 8; i++)
        {
            _frequencyBandsMaxValues[i] = 0;
        }
    }

    void PrintMicrophoneInfo()
    {
        Microphone.GetDeviceCaps(_microphoneDeviceName, out int minFreq, out int maxFreq);

        Debug.Log("minFreq " + minFreq);
        Debug.Log("maxFreq " + maxFreq);
    }

    string getMicrophoneName()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Mikrofony:");
            Debug.Log("Name: " + device);
        }

        return Microphone.devices[0];
    }

    void SetupMicrophone()
    {
        _microphoneDeviceName = getMicrophoneName();
        _micInput = GetComponent<AudioSource>();
        _micInput.clip = Microphone.Start(_microphoneDeviceName, true, 1000, 48000);
        _micInput.outputAudioMixerGroup = _micMixerGroup;
    }

    void StartCapturingMicrophoneInput()
    {
        _micInput.Play();
    }

    void FillSpectrumData()
    {
        _micInput.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int) Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2; // ogonek
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _frequencyBands[i] = average * 10;
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_frequencyBands[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _frequencyBands[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_frequencyBands[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void DetectShout()
    {
        float sum = 0;
        for (int i = 0; i < 8; i++)
        {
            sum += _bandBuffer[i];
        }

        if (sum > ShoutThreshold)
        {
            _updatesCount++;
        }
        else
        {
            _updatesCount = 0;
        }

        CurrVolume = sum;

        if (_updatesCount >= 10 && _canPerformHop)
        {
            // perform hop.
            // block performing for 800ms or so
            _canPerformHop = false;
            DG.Tweening.DOVirtual.DelayedCall(1, () =>
            {
                _updatesCount = 0;
                _canPerformHop = true;
            });
            shoutEvent.Invoke();
            _updatesCount = 0;
        }
    }

    public void AddShoutListener(UnityAction onShout)
    {
        shoutEvent.AddListener(onShout);
    }
}