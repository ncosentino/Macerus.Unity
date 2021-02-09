
using Assets.Scripts.Plugins.Features.Animations.Api;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface ISpriteAnimationBehaviourStitcher
    {
        IReadOnlySpriteAnimationBehaviour Attach(
            GameObject objectToAnimate,
            IAnimationBehavior animationBehavior);
    }
}
