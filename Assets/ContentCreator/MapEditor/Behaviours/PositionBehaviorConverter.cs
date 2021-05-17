using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class PositionBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IReadOnlyPositionBehavior;

        public bool CanConvert(Component component) => component is Transform;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IReadOnlyPositionBehavior)behavior;
            var component = target.transform;
            component.position = new Vector3((float)castedBehavior.X, (float)castedBehavior.Y);
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedComponent = (Transform)component;
            var behavior = new PositionBehavior(
                castedComponent.position.x,
                castedComponent.position.y);
            yield return behavior;
        }
    }
}
