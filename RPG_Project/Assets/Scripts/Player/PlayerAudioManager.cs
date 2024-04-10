using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlayFootStepSFX()
    {
        if(!audiosource.isPlaying)
          audiosource.PlayOneShot(SoundManager.Instance.footStepSFX);
    }

    public void PlayJumpSFX()
    {
        if(!audiosource.isPlaying)
          audiosource.PlayOneShot(SoundManager.Instance.CandleSFX);
    }

    public void PlayAllStop()
    {
        audiosource.Stop();
    }
}
