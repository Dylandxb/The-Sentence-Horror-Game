using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    private void Start()
    {
        SetVolumeLevel(slider.value);
        slider.onValueChanged.AddListener(val => SetVolumeLevel(val));
    }
    public void SetVolumeLevel(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume)*20);
    }
    
    public void SetScreenSize(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
