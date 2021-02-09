
using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class SpriteAnimationFrame : ISpriteAnimationFrame
    {
        public SpriteAnimationFrame(
            IIdentifier spriteSheetResourceId,
            IIdentifier spriteResourceId,
            bool flipVertical,
            bool flipHorizontal,
            float? durationInSeconds,
            Color color)
        {
            SpriteSheetResourceId = spriteSheetResourceId;
            SpriteResourceId = spriteResourceId;
            FlipVertical = flipVertical;
            FlipHorizontal = flipHorizontal;
            DurationInSeconds = durationInSeconds;
            Color = color;
        }

        public IIdentifier SpriteSheetResourceId { get; }

        public IIdentifier SpriteResourceId { get; }

        public bool FlipVertical { get; }

        public bool FlipHorizontal { get; }

        public float? DurationInSeconds { get; }

        public Color Color { get; }
    }
}
