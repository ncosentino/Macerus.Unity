using System.Collections.Generic;

using Macerus.Plugins.Features.GameObjects.Containers;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ContainerInteractableBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is ContainerInteractableBehavior;

        public bool CanConvert(Component component) => component is ContainerInteractableBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (ContainerInteractableBehavior)behavior;
            var component = target.AddComponent<ContainerInteractableBehaviour>();
            component.DestroyOnUse = castedBehavior.DestroyOnUse;
            component.AutomaticInteraction = castedBehavior.AutomaticInteraction;
            component.TransferItemsOnActivate = castedBehavior.TransferItemsOnActivate;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var behaviour = (ContainerInteractableBehaviour)component;
            var behavior = new ContainerInteractableBehavior(
                behaviour.AutomaticInteraction,
                behaviour.DestroyOnUse,
                behaviour.TransferItemsOnActivate);
            yield return behavior;
        }
    }
}
