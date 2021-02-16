using System;

using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Sprites;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{

    public sealed class SpriteAnimationBehaviour :
        MonoBehaviour,
        ISpriteAnimationBehaviour
    {
        private int _currentFrameIndex;
        private float _secondsElapsedOnFrame;
        private IIdentifier _lastAnimationId;

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        public IAnimationBehavior AnimationBehavior { get; set; }

        public ITimeProvider TimeProvider { get; set; }

        public ISpriteAnimationProvider SpriteAnimationProvider { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }

        public ISpriteLoader SpriteLoader { get; set; }

        IReadOnlyAnimationBehavior IReadOnlySpriteAnimationBehaviour.AnimationBehavior => AnimationBehavior;

        private void Start()
        {
            Contract.RequiresNotNull(
                AnimationBehavior,
                $"{nameof(AnimationBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SpriteRenderer,
                $"{nameof(SpriteRenderer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SpriteAnimationProvider,
                $"{nameof(SpriteAnimationProvider)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                Logger,
                $"{nameof(Logger)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                TimeProvider,
                $"{nameof(TimeProvider)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                SpriteLoader,
                $"{nameof(SpriteLoader)} was not set on '{gameObject}.{this}'.");
        }

        private void Update()
        {
            AnimationLoop();
        }

        private void AnimationLoop()
        {
            var currentAnimationId = AnimationBehavior.CurrentAnimationId;
            if (currentAnimationId == null)
            {
                SpriteRenderer.sprite = null;
                _currentFrameIndex = 0;
                _lastAnimationId = null;
                _secondsElapsedOnFrame = 0;
                return;
            }

            bool forceRefreshSprite = false;
            if (currentAnimationId != _lastAnimationId)
            {
                _currentFrameIndex = 0;
                _secondsElapsedOnFrame = 0;
                _lastAnimationId = currentAnimationId;
                forceRefreshSprite = true;
            }

            if (!SpriteAnimationProvider.TryGetAnimationById(
                currentAnimationId,
                out var currentAnimation))
            {
                throw new InvalidOperationException(
                    $"The current animation ID '{currentAnimationId}' was not " +
                    $"found for '{gameObject}.{this}'.");
            }

            if (_currentFrameIndex >= currentAnimation.Frames.Count ||
                _currentFrameIndex < 0)
            {
                throw new InvalidOperationException(
                    $"The current frame {_currentFrameIndex} was out " +
                    $"of range on '{gameObject}.{this}' animation " +
                    $"'{currentAnimationId}'.");
            }

            _secondsElapsedOnFrame += TimeProvider.SecondsSinceLastFrame;
            ISpriteAnimationFrame currentFrame;
            var lastFrameIndex = _currentFrameIndex;

            while (true)
            {
                currentFrame = currentAnimation.Frames[_currentFrameIndex];
                if (currentFrame.DurationInSeconds == null)
                {
                    break;
                }

                if (_secondsElapsedOnFrame >= currentFrame.DurationInSeconds.Value)
                {
                    _currentFrameIndex++;
                    _secondsElapsedOnFrame -= currentFrame.DurationInSeconds.Value;

                    if (_currentFrameIndex == currentAnimation.Frames.Count)
                    {
                        if (currentAnimation.Repeat)
                        {
                            _currentFrameIndex = 0;
                        }
                        else
                        {
                            AnimationBehavior.CurrentAnimationId = null;
                            return;
                        }
                    }

                    continue;
                }

                break;
            }

            if (_currentFrameIndex == lastFrameIndex &&
                !forceRefreshSprite)
            {
                return;
            }

            var sprite = SpriteLoader.SpriteFromMultiSprite(
                currentFrame.SpriteSheetResourceId,
                currentFrame.SpriteResourceId);

            SpriteRenderer.sprite = sprite;
            SpriteRenderer.flipX = currentFrame.FlipHorizontal;
            SpriteRenderer.flipY = currentFrame.FlipVertical;
            SpriteRenderer.color = currentFrame.Color;
        }
    }
}
