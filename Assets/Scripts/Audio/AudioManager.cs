using System;
using System.Threading.Tasks;

using Assets.Scripts.Behaviours.Audio;
using Assets.Scripts.Unity.Resources;

using Macerus.Plugins.Features.Audio;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Audio
{
    public sealed class AudioManager : IAudioManager
    {
        private readonly Lazy<IUnityAudioManager> _lazyUnityAudioManager;
        private readonly IResourceLoader _resourceLoader;

        public AudioManager(
            Lazy<IUnityAudioManager> lazyUnityAudioManager,
            IResourceLoader resourceLoader)
        {
            _lazyUnityAudioManager = lazyUnityAudioManager;
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
    }
}
