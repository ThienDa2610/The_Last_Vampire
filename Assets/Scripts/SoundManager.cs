using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Dash,
    Melee1,
    Melee2,
    Melee3
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    public static SoundManager instance;
    public AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volumn = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volumn);
    }
}
