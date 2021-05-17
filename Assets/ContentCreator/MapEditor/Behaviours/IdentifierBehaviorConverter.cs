using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class IdentifierBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IIdentifierBehavior;

        public bool CanConvert(Component component) => component is IdentifierBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IIdentifierBehavior)behavior;
            var component = target.AddComponent<IdentifierBehaviour>();
            component.Id = castedBehavior.Id.ToString();
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedBehaviour = (IdentifierBehaviour)component;
            var id = new StringIdentifier(castedBehaviour.Id);
            var behavior = new IdentifierBehavior(id);
            yield return behavior;
        }
    }
}
