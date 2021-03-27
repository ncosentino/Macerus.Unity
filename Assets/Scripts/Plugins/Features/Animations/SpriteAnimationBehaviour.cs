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
                    e.CurrentFrame.SpriteSheetResourceId,
                    e.CurrentFrame.SpriteResourceId);
                SpriteRenderer.sprite = sprite;
                SpriteRenderer.flipX = e.CurrentFrame.FlipHorizontal;
                SpriteRenderer.flipY = e.CurrentFrame.FlipVertical;
                SpriteRenderer.color = new Color(
                    (float)(e.CurrentFrame.Color.Red * e.AnimationMultipliers.RedMultiplier),
                    (float)(e.CurrentFrame.Color.Green * e.AnimationMultipliers.GreenMultiplier),
                    (float)(e.CurrentFrame.Color.Blue * e.AnimationMultipliers.BlueMultiplier),
                    (float)(e.CurrentFrame.Color.Alpha * e.AnimationMultipliers.AlphaMultiplier));
            });
        }
    }
}
