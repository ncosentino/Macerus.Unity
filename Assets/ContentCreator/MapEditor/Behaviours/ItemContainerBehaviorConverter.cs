using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ItemContainerBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IItemContainerBehavior;

        public bool CanConvert(Component component) => component is ItemContainerBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IItemContainerBehavior)behavior;
            var component = target.AddComponent<ItemContainerBehaviour>();
            component.ContainerId = castedBehavior.ContainerId?.ToString();
            component.ItemsReference = castedBehavior.Items;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var behaviour = (ItemContainerBehaviour)component;
            var behavior = new ItemContainerBehavior(
                string.IsNullOrWhiteSpace(behaviour.ContainerId) ? null : new StringIdentifier(behaviour.ContainerId));
            foreach (var item in behaviour.ItemsReference ?? Enumerable.Empty<IGameObject>())
            {
                if (!behavior.TryAddItem(item))
                {
                    throw new InvalidOperationException(
                        $"Could not put '{item}' into the backing behavior collection.");
                }
            }

            yield return behavior;
        }
    }
}
