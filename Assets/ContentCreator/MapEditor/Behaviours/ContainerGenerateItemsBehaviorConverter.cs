using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Containers;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ContainerGenerateItemsBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is ContainerGenerateItemsBehavior;

        public bool CanConvert(Component component) => component is ContainerGenerateItemsBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (ContainerGenerateItemsBehavior)behavior;
            var component = target.AddComponent<ContainerGenerateItemsBehaviour>();
            component.DropTableId = castedBehavior.DropTableId?.ToString();
            component.HasGeneratedItems = castedBehavior.HasGeneratedItems;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var behaviour = (ContainerGenerateItemsBehaviour)component;
            var behavior = new ContainerGenerateItemsBehavior(
                string.IsNullOrWhiteSpace(behaviour.DropTableId) ? null : new StringIdentifier(behaviour.DropTableId),
                behaviour.HasGeneratedItems);
            yield return behavior;
        }
    }
}
