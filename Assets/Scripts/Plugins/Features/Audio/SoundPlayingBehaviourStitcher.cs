using Assets.Scripts.Plugins.Features.Audio.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Audio
{
    public sealed class SoundPlayingBehaviourStitcher : ISoundPlayingBehaviourStitcher
    {
        public ICanPlaySound Attach(GameObject gameObject)
        {
            gameObject.AddComponent<AudioSource>();
            var hasFollowCameraBehaviour = gameObject.AddComponent<SoundPlayingBehaviour>();

            return hasFollowCameraBehaviour;
        }
    }
}