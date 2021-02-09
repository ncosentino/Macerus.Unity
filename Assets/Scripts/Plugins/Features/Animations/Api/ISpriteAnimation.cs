using System.Collections.Generic;

using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimation
    {
        IReadOnlyList<ISpriteAnimationFrame> Frames { get; }

        bool Repeat { get; }
    }
}
