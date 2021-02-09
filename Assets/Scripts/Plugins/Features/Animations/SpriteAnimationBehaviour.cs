using System;

using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Sprites;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{

    public sealed class SpriteAnimationBehaviour :
        MonoBehaviour,
        ISpriteAnimationBehaviour
    {
        private int _currentFrameIndex;
        private float _secondsElapsedOnFrame;

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        public IIdentifier CurrentAnimationId { get; set; }

        public ITimeProvider TimeProvider { get; set; }

        public ISpriteAnimationProvider SpriteAnimationProvider { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }

        public ISpriteLoader SpriteLoader { get; set; }

        private void Start()
        {
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
            if (CurrentAnimationId == null)
            {
                SpriteRenderer.sprite = null;
                return;
            }

            if (!SpriteAnimationProvider.TryGetAnimationById(
                CurrentAnimationId,
                out var currentAnimation))
            {
                throw new InvalidOperationException(
                    $"The current animation ID '{CurrentAnimationId}' was not " +
                    $"found for '{gameObject}.{this}'.");
            }

            if (_currentFrameIndex >= currentAnimation.Frames.Count ||
                _currentFrameIndex < 0)
            {
                throw new InvalidOperationException(
                    $"The current frame {_currentFrameIndex} was out " +
                    $"of range on '{gameObject}.{this}' animation " +
                    $"'{CurrentAnimationId}'.");
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
                            CurrentAnimationId = null;
                            return;
                        }
                    }

                    continue;
                }

                break;
            }

            if (_currentFrameIndex == lastFrameIndex)
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
