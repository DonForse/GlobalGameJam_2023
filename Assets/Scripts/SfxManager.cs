using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip[]
        DrawCardClips,
        PlayCardClips,
        ErrorClips;

    public AudioClip 
        trapClip,
        trophieClip;

    public static void AddTrophy()
    {
        Instance.PlayAddTrophy();
    }

    private void PlayAddTrophy()
    {
        AudioSource.PlayOneShot(trophieClip);
    }

    public static SfxManager Instance => FindObjectOfType<SfxManager>();

    public static void AddCard()
    {
        Instance.PlayAddCard();
    }

    private void PlayAddCard()
    {
        var clip = PickRandomClipFrom(PlayCardClips);
        AudioSource.PlayOneShot(clip);
    }

    private AudioClip PickRandomClipFrom(AudioClip[] clips) => clips[Random.Range(0, clips.Length)];
}
