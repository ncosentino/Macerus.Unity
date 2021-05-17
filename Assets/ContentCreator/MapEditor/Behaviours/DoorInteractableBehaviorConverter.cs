using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Static.Doors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class DoorInteractableBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is DoorInteractableBehavior;

        public bool CanConvert(Component component) => component is DoorBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (DoorInteractableBehavior)behavior;
            var component = target.AddComponent<DoorBehaviour>();
            component.AutomaticInteraction = castedBehavior.AutomaticInteraction;
            component.TransitionToMapId = castedBehavior.TransitionToMapId?.ToString();
            component.HasPositionTransition =
                castedBehavior.TransitionToX.HasValue &&
                castedBehavior.TransitionToY.HasValue;
            component.TransitionToX = castedBehavior.TransitionToX.HasValue
                ? (float)castedBehavior.TransitionToX.Value 
                : 0f;
            component.TransitionToY = castedBehavior.TransitionToY.HasValue
                ? (float)castedBehavior.TransitionToY.Value
                : 0f;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedBehaviour = (DoorBehaviour)component;
            var behavior = new DoorInteractableBehavior(
                castedBehaviour.AutomaticInteraction,
                castedBehaviour.TransitionToMapId == null
                    ? null 
                    : new StringIdentifier(castedBehaviour.TransitionToMapId),
                castedBehaviour.HasPositionTransition ? castedBehaviour.TransitionToX : 0f,
                castedBehaviour.HasPositionTransition ? castedBehaviour.TransitionToY : 0f);
            yield return behavior;
        }
    }
}
