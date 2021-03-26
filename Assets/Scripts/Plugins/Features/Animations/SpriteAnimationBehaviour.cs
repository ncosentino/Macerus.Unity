using System;
using System.Threading.Tasks;

using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Resources.Sprites;
using Assets.Scripts.Unity.Threading;

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
        private float _triggerTime;
        private int _currentFrameIndex;
        private float _secondsElapsedOnFrame;
        private IIdentifier _lastAnimationId;

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        public IAnimationBehavior AnimationBehavior { get; set; }

        public IDynamicAnimationBehavior DynamicAnimationBehavior { get; set; }

        public ITimeProvider TimeProvider { get; set; }

        public ISpriteAnimationProvider SpriteAnimationProvider { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }

        public ISpriteLoader SpriteLoader { get; set; }

        public TimeSpan UpdateDelay { get; set; }
        
        public IDispatcher Dispatcher { get; set; }

        IReadOnlyAnimationBehavior IReadOnlySpriteAnimationBehaviour.AnimationBehavior => AnimationBehavior;

        IReadOnlyDynamicAnimationBehavior IReadOnlySpriteAnimationBehaviour.DynamicAnimationBehavior => DynamicAnimationBehavior;

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, SpriteLoader, nameof(SpriteLoader));
            UnityContracts.RequiresNotNull(this, TimeProvider, nameof(TimeProvider));
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, SpriteAnimationProvider, nameof(SpriteAnimationProvider));
            UnityContracts.RequiresNotNull(this, SpriteRenderer, nameof(SpriteRenderer));
            UnityContracts.RequiresNotNull(this, AnimationBehavior, nameof(AnimationBehavior));
            UnityContracts.RequiresNotNull(this, Dispatcher, nameof(Dispatcher));
            Contract.Requires(
                UpdateDelay.TotalSeconds > 0,
                $"'{nameof(UpdateDelay)}' must be set on '{transform.gameObject}.{this}'.");
        }

        private async void FixedUpdate()
        {
            var secondsSinceLastFrame = TimeProvider.SecondsSinceLastFrame;
            if (secondsSinceLastFrame < _triggerTime)
            {
                return;
            }

            await AnimationLoopAsync(secondsSinceLastFrame);
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)UpdateDelay.TotalSeconds;
        }

        private async Task AnimationLoopAsync(float secondsSinceLastFrame)
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

            await Task.Run(() =>
            {               
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

                _secondsElapsedOnFrame += secondsSinceLastFrame;
                ISpriteAnimationFrame currentFrame;
                var lastFrameIndex = _currentFrameIndex;

                var animationSpeedMultiplier = DynamicAnimationBehavior?.AnimationSpeedMultiplier ?? 1;

                while (true)
                {
                    currentFrame = currentAnimation.Frames[_currentFrameIndex];
                    if (currentFrame.DurationInSeconds == null)
                    {
                        break;
                    }

                    var durationInSeconds = (float)(currentFrame.DurationInSeconds.Value / animationSpeedMultiplier);
                    if (_secondsElapsedOnFrame >= durationInSeconds)
                    {
                        _currentFrameIndex++;
                        _secondsElapsedOnFrame -= durationInSeconds;

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

                var red = (float)(DynamicAnimationBehavior?.RedMultiplier ?? 1);
                var green = (float)(DynamicAnimationBehavior?.GreenMultiplier ?? 1);
                var blue = (float)(DynamicAnimationBehavior?.BlueMultiplier ?? 1);
                var alpha = (float)(DynamicAnimationBehavior?.AlphaMultiplier ?? 1);

                Dispatcher.RunOnMainThread(() =>
                {
                    if (SpriteRenderer == null)
                    {
                        return;
                    }

                    var sprite = SpriteLoader.SpriteFromMultiSprite(
                        currentFrame.SpriteSheetResourceId,
                        currentFrame.SpriteResourceId);
                    SpriteRenderer.sprite = sprite;
                    SpriteRenderer.flipX = currentFrame.FlipHorizontal;
                    SpriteRenderer.flipY = currentFrame.FlipVertical;
                    SpriteRenderer.color = new Color(
                        currentFrame.Color.r * red,
                        currentFrame.Color.g * green,
                        currentFrame.Color.b * blue,
                        currentFrame.Color.a * alpha);
                });
            });
        }
    }
}
