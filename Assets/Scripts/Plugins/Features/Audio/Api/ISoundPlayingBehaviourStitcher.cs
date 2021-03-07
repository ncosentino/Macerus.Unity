using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Audio.Api
{
    public interface ISoundPlayingBehaviourStitcher
    {
        ICanPlaySound Attach(GameObject gameObject);
    }
}
