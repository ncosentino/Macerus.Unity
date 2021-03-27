using System.Threading.Tasks;

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
            var redTask = Task.Run(() => (float)(DynamicAnimationBehavior?.RedMultiplier ?? 1));
            var greenTask = Task.Run(() => (float)(DynamicAnimationBehavior?.GreenMultiplier ?? 1));
            var blueTask = Task.Run(() => (float)(DynamicAnimationBehavior?.BlueMultiplier ?? 1));
            var alphaTask = Task.Run(() => (float)(DynamicAnimationBehavior?.AlphaMultiplier ?? 1));
            
            Task.WaitAll(redTask, greenTask, blueTask, alphaTask);
            var red = redTask.Result;
            var green = greenTask.Result;
            var blue = blueTask.Result;
            var alpha = alphaTask.Result;
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
