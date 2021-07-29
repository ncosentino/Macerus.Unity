using System;

using UnityEngine;

namespace Assets.Scripts.Behaviours.Audio
{
    public sealed class AudioManagerBehaviour :
        MonoBehaviour,
        IUnityAudioManager
    {
        private static AudioManagerBehaviour _instance;

        private AudioSource _musicSource;
        private AudioSource _effectsSource;

        public static IUnityAudioManager Instance => _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_instance == null)
            {
                var gameObject = new GameObject("AudioManager");
                _instance = gameObject.AddComponent<AudioManagerBehaviour>();
                DontDestroyOnLoad(_instance.gameObject);
            }
        }

        public void PlayMusic(AudioClip musicClip) =>
            PlayAudio(_musicSource, musicClip);

        public void PlayEffect(AudioClip audioClip) =>
            PlayAudio(_effectsSource, audioClip);

        private void PlayAudio(
            AudioSource audioSource,
            AudioClip audioClip)
        {
            if (audioSource.clip == audioClip)
            {
                return;
            }

            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        private void Start()
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.volume = 0.5f;

            _effectsSource = gameObject.AddComponent<AudioSource>();
            _effectsSource.loop = false;
            _effectsSource.volume = 0.5f;
        }
    }
}
