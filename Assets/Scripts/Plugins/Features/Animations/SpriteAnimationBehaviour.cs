using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity.Resources.Sprites;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{

    public sealed class SpriteAnimationBehaviour :
        MonoBehaviour,
        ISpriteAnimationBehaviour
    {
        public IAnimationBehavior AnimationBehavior { get; set; }

        public IObservableDynamicAnimationBehavior DynamicAnimationBehavior { get; set; }

        public SpriteRenderer SpriteRenderer { get; set; }

        public ISpriteLoader SpriteLoader { get; set; }
        
        public IDispatcher Dispatcher { get; set; }

        IReadOnlyAnimationBehavior IReadOnlySpriteAnimationBehaviour.AnimationBehavior => AnimationBehavior;

        IObservableDynamicAnimationBehavior IReadOnlySpriteAnimationBehaviour.DynamicAnimationBehavior => DynamicAnimationBehavior;

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, SpriteLoader, nameof(SpriteLoader));
            UnityContracts.RequiresNotNull(this, SpriteRenderer, nameof(SpriteRenderer));
            UnityContracts.RequiresNotNull(this, AnimationBehavior, nameof(AnimationBehavior));
            UnityContracts.RequiresNotNull(this, Dispatcher, nameof(Dispatcher));

            DynamicAnimationBehavior.AnimationFrameChanged += DynamicAnimationBehavior_AnimationFrameChanged;
        }

        private void OnDestroy()
        {
            if (DynamicAnimationBehavior != null)
            {
                DynamicAnimationBehavior.AnimationFrameChanged -= DynamicAnimationBehavior_AnimationFrameChanged;
            }
        }

        private void DynamicAnimationBehavior_AnimationFrameChanged(object sender, AnimationFrameEventArgs e)
        {
            var red = (float)(DynamicAnimationBehavior?.RedMultiplier ?? 1);
            var green = (float)(DynamicAnimationBehavior?.GreenMultiplier ?? 1);
            var blue = (float)(DynamicAnimationBehavior?.BlueMultiplier ?? 1);
            var alpha = (float)(DynamicAnimationBehavior?.AlphaMultiplier ?? 1);
            var currentFrame = e.CurrentFrame;

            Dispatcher.RunOnMainThread(() =>
            {
                if (SpriteRenderer == null)
                {
                    return;
                }

                if (e.CurrentFrame == null)
                {
                    SpriteRenderer.sprite = null;
                    return;
                }

                var sprite = SpriteLoader.SpriteFromMultiSprite(
                    currentFrame.SpriteSheetResourceId,
                    currentFrame.SpriteResourceId);
                SpriteRenderer.sprite = sprite;
                SpriteRenderer.flipX = currentFrame.FlipHorizontal;
                SpriteRenderer.flipY = currentFrame.FlipVertical;
                SpriteRenderer.color = new Color(
                    (float)(currentFrame.Color.Red * red),
                    (float)(currentFrame.Color.Green * green),
                    (float)(currentFrame.Color.Blue * blue),
                    (float)(currentFrame.Color.Alpha * alpha));
            });
        }
    }
}
