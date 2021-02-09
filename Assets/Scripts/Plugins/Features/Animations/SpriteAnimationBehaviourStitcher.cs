
using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Sprites;

using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class SpriteAnimationBehaviourStitcher : ISpriteAnimationBehaviourStitcher
    {
        private readonly ISpriteLoader _spriteLoader;
        private readonly ISpriteAnimationProvider _spriteAnimationProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly ProjectXyz.Api.Logging.ILogger _logger;

        public SpriteAnimationBehaviourStitcher(
            ISpriteAnimationProvider spriteAnimationProvider,
            ISpriteLoader spriteLoader,
            ITimeProvider timeProvider,
            ProjectXyz.Api.Logging.ILogger logger)
        {
            _spriteAnimationProvider = spriteAnimationProvider;
            _spriteLoader = spriteLoader;
            _timeProvider = timeProvider;
            _logger = logger;
        }

        public IReadOnlySpriteAnimationBehaviour Attach(GameObject objectToAnimate)
        {
            var spriteAnimationBehaviour = objectToAnimate.AddComponent<SpriteAnimationBehaviour>();
            spriteAnimationBehaviour.Logger = _logger;
            spriteAnimationBehaviour.CurrentAnimationId = null;
            spriteAnimationBehaviour.TimeProvider = _timeProvider;
            spriteAnimationBehaviour.SpriteRenderer = objectToAnimate.GetRequiredComponent<SpriteRenderer>();
            spriteAnimationBehaviour.SpriteLoader = _spriteLoader;
            spriteAnimationBehaviour.SpriteAnimationProvider = _spriteAnimationProvider;

            // FIXME: just for testing
            spriteAnimationBehaviour.CurrentAnimationId = new StringIdentifier("player_walk_back");

            return spriteAnimationBehaviour;
        }
    }
}
