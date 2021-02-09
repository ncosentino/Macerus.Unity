using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Sprites;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationBehaviour : IReadOnlySpriteAnimationBehaviour
    {
        new IIdentifier CurrentAnimationId { get; set; }

        ISpriteAnimationProvider SpriteAnimationProvider { get; set; }

        ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        ISpriteLoader SpriteLoader { get; set; }

        SpriteRenderer SpriteRenderer { get; set; }

        ITimeProvider TimeProvider { get; set; }
    }
}
