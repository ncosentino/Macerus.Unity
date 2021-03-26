using Assets.Scripts.Unity.Resources.Sprites;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationBehaviour : IReadOnlySpriteAnimationBehaviour
    {
        new IAnimationBehavior AnimationBehavior { get; set; }

        new IObservableDynamicAnimationBehavior DynamicAnimationBehavior { get; set; }

        ISpriteLoader SpriteLoader { get; set; }

        SpriteRenderer SpriteRenderer { get; set; }

        IDispatcher Dispatcher { get; set; }
    }
}
