using Assets.Scripts.Plugins.Features.Audio.Api;
using System;
using System.Linq;
using UnityEngine;

public class SoundPlayingBehaviour : MonoBehaviour, ICanPlaySound
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlaySound(float[] wave)
    {
        var audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            return;
        }

        var audioClip = AudioClip.Create(Guid.NewGuid().ToString(), wave.Length, 1, 48000, false);
        audioClip.SetData(wave, 0);

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
