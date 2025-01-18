using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager Instance;

    [SerializeField] private SoundLibrary sfxLibrary;
    [SerializeField] private AudioSource sfx2DSource;

    private const string VolumeKey = "SFXVolume";
    private float volume = 1.0f;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        volume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);
        ApplyVolume();
    }
    private void ApplyVolume()
    {
        sfx2DSource.volume = volume;
    }
    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        ApplyVolume();
        PlayerPrefs.SetFloat(VolumeKey, volume); 
        PlayerPrefs.Save();
    }
    public void PlaySound3D(AudioClip clip, Vector3 position)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, position);
    }
    public void PlaySound3D(string name, Vector3 position)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(name), position);
    }
    public void PlaySound2D(string name)
    {
        //Debug.Log(name);
        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(name));
    }
}
