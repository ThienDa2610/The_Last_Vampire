using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sfxSlider;  
    public Slider musicSlider;

    private const string SFXVolumeKey = "SFXVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private void Start()
    {
        
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f); 
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f); 

        SetSFX(savedSFXVolume);
        SetMusic(savedMusicVolume);

        sfxSlider.value = savedSFXVolume;
        musicSlider.value = savedMusicVolume;

        sfxSlider.onValueChanged.AddListener(SetSFX);
        musicSlider.onValueChanged.AddListener(SetMusic);
    }

    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
    }
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }
}
