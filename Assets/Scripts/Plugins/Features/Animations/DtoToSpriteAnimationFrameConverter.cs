
using Assets.Scripts.Plugins.Features.Animations;

using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class DtoToSpriteAnimationFrameConverter : IDtoToSpriteAnimationFrameConverter
    {
        public ISpriteAnimationFrame Convert(SpriteAnimationFrameDto dto)
        {
            var spriteAnimationFrame = new SpriteAnimationFrame(
                new StringIdentifier(dto.SpriteSheetResourceId),
                new StringIdentifier(dto.SpriteResourceId),
                dto.FlipVertical,
                dto.FlipHorizontal,
                dto.DurationInSeconds,
                new Color(
                    dto.Red,
                    dto.Green,
                    dto.Blue,
                    dto.Alpha));
            return spriteAnimationFrame;
        }
    }
}
