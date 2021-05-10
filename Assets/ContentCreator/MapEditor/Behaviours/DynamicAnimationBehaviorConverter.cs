using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Api;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class DynamicAnimationBehaviorConverter : IDiscoverableBehaviorConverter
    {
        private readonly IDynamicAnimationBehaviorFactory _dynamicAnimationBehaviorFactory;

        public DynamicAnimationBehaviorConverter(IDynamicAnimationBehaviorFactory dynamicAnimationBehaviorFactory)
        {
            _dynamicAnimationBehaviorFactory = dynamicAnimationBehaviorFactory;
        }

        public bool CanConvert(IBehavior behavior) => behavior is IReadOnlyDynamicAnimationBehavior;

        public bool CanConvert(Component component) => component is DynamicAnimationBehaviour;

        public Component Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IReadOnlyDynamicAnimationBehavior)behavior;
            var component = target.AddComponent<DynamicAnimationBehaviour>();
            component.AnimationId = castedBehavior.CurrentAnimationId?.ToString();
            component.Visible = castedBehavior.Visible;
            component.SourcePattern = castedBehavior.SourcePattern;
            return component;
        }

        public IBehavior Convert(Component component)
        {
            var castedBehaviour = (DynamicAnimationBehaviour)component;
            var behavior = _dynamicAnimationBehaviorFactory.Create(
                castedBehaviour.SourcePattern,
                new StringIdentifier(castedBehaviour.AnimationId ?? string.Empty),
                castedBehaviour.Visible,
                0);
            return behavior;
        }
    }
}