using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public AudioClip footStepSFX;
    public AudioClip BuzzingLightSFX;
    public AudioClip CandleSFX;

    public AudioClip typeText;

    private new AudioSource audio => GetComponent<AudioSource>();

    public void PlaySFX(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        audio.Stop();
    }
}
