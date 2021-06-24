using System.Threading.Tasks;

using Assets.Scripts.Plugins.Features.Animations.Api;
using Assets.Scripts.Unity.Resources.Sprites;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Animations.Api;

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

        private async Task Start()
        {
            UnityContracts.RequiresNotNull(this, SpriteLoader, nameof(SpriteLoader));
            UnityContracts.RequiresNotNull(this, SpriteRenderer, nameof(SpriteRenderer));
            UnityContracts.RequiresNotNull(this, AnimationBehavior, nameof(AnimationBehavior));
            UnityContracts.RequiresNotNull(this, Dispatcher, nameof(Dispatcher));

            DynamicAnimationBehavior.AnimationFrameChanged += DynamicAnimationBehavior_AnimationFrameChanged;
            UpdateSprite(
                DynamicAnimationBehavior.CurrentFrame,
                await DynamicAnimationBehavior.GetAnimationMultipliersAsync());
        }

        private void OnDestroy()
        {
            if (DynamicAnimationBehavior != null)
            {
                DynamicAnimationBehavior.AnimationFrameChanged -= DynamicAnimationBehavior_AnimationFrameChanged;
            }
        }

        private void UpdateSprite(
            ISpriteAnimationFrame currentFrame,
            IAnimationMultipliers animationMultipliers)
        {
            if (SpriteRenderer == null)
            {
                return;
            }

            if (currentFrame == null)
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
                (float)(currentFrame.Color.Red * animationMultipliers.RedMultiplier),
                (float)(currentFrame.Color.Green * animationMultipliers.GreenMultiplier),
                (float)(currentFrame.Color.Blue * animationMultipliers.BlueMultiplier),
                (float)(currentFrame.Color.Alpha * animationMultipliers.AlphaMultiplier));
        }

        private void DynamicAnimationBehavior_AnimationFrameChanged(
            object sender,
            AnimationFrameEventArgs e) => Dispatcher.RunOnMainThread(() =>
            {
                UpdateSprite(e.CurrentFrame, e.AnimationMultipliers);
            });
    }
}
