using UnityEngine;

namespace Assets.Scripts.Behaviours.Audio
{

    public sealed class AudioManagerBehaviour :
        MonoBehaviour,
        IUnityAudioManager
    {
        private static AudioManagerBehaviour _instance;

        private AudioSource _musicSource;

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

        public void PlayMusic(AudioClip musicClip)
        {
            if (_musicSource.clip == musicClip)
            {
                return;
            }

            _musicSource.Stop();
            _musicSource.clip = musicClip;
            _musicSource.Play();
        }

        private void Start()
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.volume = 0.5f;
        }
    }
}
