using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    private const string MusicVolumeKey = "MusicVolume";
    private float volume = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        volume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        musicSource.volume = volume;
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        ApplyVolume(); 
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);  
        PlayerPrefs.Save(); 
    }

    public void PlayMusic(string name, float fadeDuration = 0.5f)
    {
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(name), fadeDuration));
    }
    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }

}
