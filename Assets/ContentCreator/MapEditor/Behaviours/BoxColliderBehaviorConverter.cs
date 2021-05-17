using System.Collections.Generic;

using Macerus.Api.Behaviors;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class BoxColliderBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IBoxColliderBehavior;

        public bool CanConvert(Component component) => component is BoxCollider2D;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IBoxColliderBehavior)behavior;
            var component = target.AddComponent<BoxCollider2D>();
            component.size = new Vector2(
                (float)castedBehavior.Width, 
                (float)castedBehavior.Height);
            component.offset = new Vector2(
                (float)castedBehavior.X,
                (float)castedBehavior.Y);
            component.isTrigger = castedBehavior.IsTrigger;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedComponent = (BoxCollider2D)component;
            var behavior = new BoxColliderBehavior(
                castedComponent.offset.x,
                castedComponent.offset.y,
                castedComponent.size.x,
                castedComponent.size.y,
                castedComponent.isTrigger);
            yield return behavior;
        }
    }
}
