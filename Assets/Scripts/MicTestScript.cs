using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MicTestScript : MonoBehaviour
{
    AudioSource _micInput;
    public AudioMixerGroup _micMixerGroup;
    public static float[] _samples = new float[512];
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ELO SZYMEK - START");

        Microphone.GetDeviceCaps(getMicrophoneName(), out int minFreq, out int maxFreq);

        Debug.Log("minFreq " + minFreq);
        Debug.Log("maxFreq " + maxFreq);

        _micInput = GetComponent<AudioSource>();
        _micInput.clip = Microphone.Start(getMicrophoneName(), true, 10, 48000);

        _micInput.outputAudioMixerGroup = _micMixerGroup;
        
        _micInput.Play();
    }

    // Update is called once per frame
    void Update()
    {
        _micInput.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void printDebugs()
    {
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
