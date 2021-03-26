using System;

using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity;
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
        private readonly ISpriteAnimationProvider _spriteAnimationProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly ProjectXyz.Api.Logging.ILogger _logger;
        private readonly IDispatcher _dispatcher;

        public SpriteAnimationBehaviourStitcher(
            ISpriteAnimationProvider spriteAnimationProvider,
            ISpriteLoader spriteLoader,
            ITimeProvider timeProvider,
            ProjectXyz.Api.Logging.ILogger logger,
            IDispatcher dispatcher)
        {
            _spriteAnimationProvider = spriteAnimationProvider;
            _spriteLoader = spriteLoader;
            _timeProvider = timeProvider;
            _logger = logger;
            _dispatcher = dispatcher;
        }

        public IReadOnlySpriteAnimationBehaviour Attach(
            GameObject objectToAnimate,
            IAnimationBehavior animationBehavior)
        {
            var spriteAnimationBehaviour = objectToAnimate.AddComponent<SpriteAnimationBehaviour>();
            spriteAnimationBehaviour.Logger = _logger;
            spriteAnimationBehaviour.TimeProvider = _timeProvider;
            spriteAnimationBehaviour.SpriteLoader = _spriteLoader;
            spriteAnimationBehaviour.SpriteAnimationProvider = _spriteAnimationProvider;
            spriteAnimationBehaviour.Dispatcher = _dispatcher;
            spriteAnimationBehaviour.SpriteRenderer = objectToAnimate.GetRequiredComponent<SpriteRenderer>();
            spriteAnimationBehaviour.AnimationBehavior = animationBehavior;
            spriteAnimationBehaviour.DynamicAnimationBehavior = animationBehavior as IDynamicAnimationBehavior;
            spriteAnimationBehaviour.UpdateDelay = TimeSpan.FromSeconds(0.1);

            return spriteAnimationBehaviour;
        }
    }
}
