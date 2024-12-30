using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MusicTrack
{
    public string name;
    public AudioClip clip;
}

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;

    public AudioClip GetClipFromName(string name)
    {
        foreach (var track in tracks)
        {
            if (track.name == name)
                return track.clip;
        }
        return null;
    }
}
