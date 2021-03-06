﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput2 : MonoBehaviour
{

    public float MedianPercent = 0.7f;
    
    #region SingleTon
 
    public static MicInput2 Inctance { set; get; }
 
    #endregion
 
    public static float MicLoudness;
    public static float MicLoudnessinDecibels;
 
    private string _device;
    
    private List<float> Peaks = new List<float>(128);
 
    //mic initialization
    public void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];
        }
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
        _isInitialized = true;
    }
 
    public void StopMicrophone()
    {
        Microphone.End(_device);
        _isInitialized = false;
    }
 
 
    AudioClip _clipRecord = new AudioClip();
    int _sampleWindow = 128;
 
    //get data from microphone into audioclip
    float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        Peaks.Clear();
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            Peaks.Add(wavePeak);
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        return MedianCalc.Median(Peaks,MedianPercent);
        
        return levelMax;
    }
 
    //get data from microphone into audioclip
    float MicrophoneLevelMaxDecibels()
    {
 
        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));
 
        return db;
    }
 
    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = MicrophoneLevelMax();
        MicLoudnessinDecibels = MicrophoneLevelMaxDecibels();
    }
 
    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
        Inctance = this;
    }
 
    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }
 
    void OnDestroy()
    {
        StopMicrophone();
    }
 
 
    // make sure the mic gets started & stopped when application gets focused
    #if !UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");
 
            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
 
        }
    }
    #endif
}