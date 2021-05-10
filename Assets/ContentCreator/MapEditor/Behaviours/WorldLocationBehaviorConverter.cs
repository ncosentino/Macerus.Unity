
using Macerus.Api.Behaviors;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class WorldLocationBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IReadOnlyWorldLocationBehavior;

        public bool CanConvert(Component component) => component is Transform;

        public Component Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IReadOnlyWorldLocationBehavior)behavior;
            var component = target.transform;
            component.position = new Vector3((float)castedBehavior.X, (float)castedBehavior.Y);
            component.localScale = new Vector3((float)castedBehavior.Width, (float)castedBehavior.Height);
            return component;
        }

        public IBehavior Convert(Component component)
        {
            var castedComponent = (Transform)component;

            var behavior = new WorldLocationBehavior()
            {
                X = castedComponent.position.x,
                Y = castedComponent.position.y,
                Width = castedComponent.localScale.x,
                Height = castedComponent.localScale.y,
            };
            return behavior;
        }
    }
}
