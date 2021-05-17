using System.Collections.Generic;

using Macerus.Api.Behaviors;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class CircleColliderBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is ICircleColliderBehavior;

        public bool CanConvert(Component component) => component is CircleCollider2D;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (ICircleColliderBehavior)behavior;
            var component = target.AddComponent<CircleCollider2D>();
            component.radius = (float)castedBehavior.Radius;
            component.offset = new Vector2(
                (float)castedBehavior.X,
                (float)castedBehavior.Y);
            component.isTrigger = castedBehavior.IsTrigger;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedComponent = (CircleCollider2D)component;
            var behavior = new CircleColliderBehavior(
                castedComponent.offset.x,
                castedComponent.offset.y,
                castedComponent.radius,
                castedComponent.isTrigger);
            yield return behavior;
        }
    }
}
