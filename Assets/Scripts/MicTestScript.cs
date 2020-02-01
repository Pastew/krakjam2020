using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class MicTestScript : MonoBehaviour
{
    AudioSource _micInput;
    public AudioMixerGroup _micMixerGroup;

    public static float[] _samples = new float[512];
    
    public static float[] _frequencyBands = new float[8]; // at the moment
    public static float[] _bandBuffer = new float[8]; // at the moment
    float[] _bufferDecrease = new float[8];

    public static float[] _frequencyBandsMaxValues = new float[8];


    public bool _shoutDetected;

    int _updatesCount;
    bool _canPerformHop = true;

    public bool _calibrationPeriodOn;

    public float _shoutThreshold = 0;
    public float _currVolume = 0;

    public int shoutPeriodMilisec;

    public List<ParamCube> myCubes;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            _frequencyBandsMaxValues[i] = 0;
        }

        Debug.Log("ELO SZYMEK - START");

        Microphone.GetDeviceCaps(getMicrophoneName(), out int minFreq, out int maxFreq);

        Debug.Log("minFreq " + minFreq);
        Debug.Log("maxFreq " + maxFreq);

        _micInput = GetComponent<AudioSource>();
        _micInput.clip = Microphone.Start(getMicrophoneName(), true, 1000, 48000);

        _micInput.outputAudioMixerGroup = _micMixerGroup;
        
        _micInput.Play();

        _calibrationPeriodOn = true;
        DG.Tweening.DOVirtual.DelayedCall(2, () => { _calibrationPeriodOn = false; });

        // find cubes
        myCubes = FindObjectsOfType<ParamCube>().ToList();
    }

    void MakeFrequencyBands()
    {
        /*
         * 48000 / 512 = 93hz/sample
         * 
         * 0: 0-93hz (1)
         * 1
         * 2
         * 3
         * 4
         * 5
         * 6
         * 7
         */
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            
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

            if (_calibrationPeriodOn)
            { 
                _frequencyBandsMaxValues[i] = Mathf.Max(_frequencyBands[i], _frequencyBandsMaxValues[i]);
            }
        }

        if (_calibrationPeriodOn)
        {
            float _currThreshold = 0;
            for (int i = 0; i < 8; i++)
            {
                _currThreshold += _frequencyBandsMaxValues[i];
            }
            //_currThreshold *= (float)1.5;
            _shoutThreshold = Mathf.Max(_shoutThreshold, _currThreshold);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _micInput.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        MakeFrequencyBands();

        // assuming 60/1000ms
        // 6 updates in shout = shout

        BandBuffer();

        DetectShout2();
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

    void DetectShout2()
    {
        if (_calibrationPeriodOn)
        {
            return;
        }

        float sum = 0;
        for (int i = 0; i < 8; i++)
        {
            sum += _bandBuffer[i];
        }

        if (sum > _shoutThreshold)
        {
            _updatesCount++;
        }
        else
        {
            _updatesCount = 0;
            _shoutDetected = false;
        }

        _currVolume = sum;

        if (_updatesCount >= 10 && _canPerformHop)
        {
            // perform hop.
            myCubes.ForEach((c) => { c.Hop(); });
            // block performing for 800ms or so
            _canPerformHop = false;
            DG.Tweening.DOVirtual.DelayedCall(1, () => { _updatesCount = 0; _canPerformHop = true; });
            _updatesCount = 0;
        }
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
}
