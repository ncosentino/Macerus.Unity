using UnityEngine;

namespace Assets.Scripts.Behaviours.Audio
{
    public interface IUnityAudioManager
    {
        void PlayMusic(AudioClip musicClip);

        void PlayEffect(AudioClip audioClip);
    }
}
