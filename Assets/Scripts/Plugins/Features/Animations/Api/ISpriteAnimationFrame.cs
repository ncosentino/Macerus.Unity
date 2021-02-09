
using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface ISpriteAnimationFrame
    {
        Color Color { get; }

        float? DurationInSeconds { get; }

        bool FlipHorizontal { get; }

        bool FlipVertical { get; }

        IIdentifier SpriteSheetResourceId { get; }

        IIdentifier SpriteResourceId { get; }
    }
}
