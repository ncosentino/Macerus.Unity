using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Plugins.Features.Animations.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class SpriteAnimation : ISpriteAnimation
    {
        public SpriteAnimation(
            IEnumerable<ISpriteAnimationFrame> frames,
            bool repeat)
        {
            Frames = frames.ToArray();
            Repeat = repeat;
        }

        public IReadOnlyList<ISpriteAnimationFrame> Frames { get; }

        public bool Repeat { get; }
    }
}
