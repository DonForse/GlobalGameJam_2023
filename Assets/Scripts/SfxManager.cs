using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource AudioSource;

    public List<AudioClip>
        DrawCardClips,
        PlayCardClips,
        ErrorClips;

    public AudioClip 
        trapClip,
        trophieClip;
}