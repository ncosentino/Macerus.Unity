using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Sprites;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationBehaviour : IReadOnlySpriteAnimationBehaviour
    {
        new IAnimationBehavior AnimationBehavior { get; set; }

        ISpriteAnimationProvider SpriteAnimationProvider { get; set; }

        ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        ISpriteLoader SpriteLoader { get; set; }

        SpriteRenderer SpriteRenderer { get; set; }

        ITimeProvider TimeProvider { get; set; }
    }
}
