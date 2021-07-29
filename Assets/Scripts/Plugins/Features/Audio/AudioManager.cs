using System;
using System.Threading.Tasks;

using Assets.Scripts.Behaviours.Audio;
using Assets.Scripts.Unity.Resources;

using Macerus.Plugins.Features.Audio;
using Macerus.Plugins.Features.Audio.SoundGeneration;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Audio
{
    public sealed class AudioManager : IAudioManager
    {
        private readonly Lazy<IUnityAudioManager> _lazyUnityAudioManager;
        private readonly Lazy<ISoundGenerator> _lazySoundGenerator;
        private readonly Lazy<IFilterContextProvider> _lazyFilterContextProvider;
        private readonly IResourceLoader _resourceLoader;

        public AudioManager(
            Lazy<IUnityAudioManager> lazyUnityAudioManager,
            Lazy<ISoundGenerator> lazySoundGenerator,
            Lazy<IFilterContextProvider> lazyFilterContextProvider,
            IResourceLoader resourceLoader)
        {
            _lazyUnityAudioManager = lazyUnityAudioManager;
            _lazySoundGenerator = lazySoundGenerator;
            _lazyFilterContextProvider = lazyFilterContextProvider;
            _resourceLoader = resourceLoader;
        }

        public async Task PlayMusicAsync(IIdentifier musicResourceId)
        {
            // FIXME: instead of ToString(), we perform a lookup or something
            var relativeResourcePath = musicResourceId.ToString();
            var musicClip = await _resourceLoader
                .LoadAsync<AudioClip>(relativeResourcePath)
                .ConfigureAwait(false);
            _lazyUnityAudioManager.Value.PlayMusic(musicClip);
        }

        public async Task PlaySoundEffectAsync(IIdentifier soundEffectResourceId)
        {
            // FIXME: actually look up SFX instead of randomly creating new ones :)
            var soundWaveform = await Task.Run(() =>
            {
                var filterContext = _lazyFilterContextProvider.Value.GetContext();
                return _lazySoundGenerator.Value.GenerateSound(filterContext);
            });

            var audioClip = AudioClip.Create(Guid.NewGuid().ToString(), soundWaveform.Length, 1, 48000, false);
            audioClip.SetData(soundWaveform, 0);
            _lazyUnityAudioManager.Value.PlayEffect(audioClip);
        }
    }
}
