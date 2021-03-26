using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Sprites;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class SpriteAnimationBehaviourStitcher : ISpriteAnimationBehaviourStitcher
    {
        private readonly ISpriteLoader _spriteLoader;
        private readonly IDispatcher _dispatcher;

        public SpriteAnimationBehaviourStitcher(
            ISpriteLoader spriteLoader,
            IDispatcher dispatcher)
        {
            _spriteLoader = spriteLoader;
            _dispatcher = dispatcher;
        }

        public IReadOnlySpriteAnimationBehaviour Attach(
            GameObject objectToAnimate,
            IAnimationBehavior animationBehavior)
        {
            var spriteAnimationBehaviour = objectToAnimate.AddComponent<SpriteAnimationBehaviour>();
            spriteAnimationBehaviour.SpriteLoader = _spriteLoader;
            spriteAnimationBehaviour.Dispatcher = _dispatcher;
            spriteAnimationBehaviour.SpriteRenderer = objectToAnimate.GetRequiredComponent<SpriteRenderer>();
            spriteAnimationBehaviour.AnimationBehavior = animationBehavior;
            spriteAnimationBehaviour.DynamicAnimationBehavior = animationBehavior as IDynamicAnimationBehavior;

            return spriteAnimationBehaviour;
        }
    }
}
