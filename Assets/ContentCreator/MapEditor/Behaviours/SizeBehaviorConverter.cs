using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class SizeBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IReadOnlySizeBehavior;

        public bool CanConvert(Component component) => component is Transform;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IReadOnlySizeBehavior)behavior;
            var component = target.transform;
            component.localScale = new Vector3((float)castedBehavior.Width, (float)castedBehavior.Height);
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedComponent = (Transform)component;
            var behavior = new SizeBehavior(
                castedComponent.localScale.x,
                castedComponent.localScale.y);
            yield return behavior;
        }
    }
}
