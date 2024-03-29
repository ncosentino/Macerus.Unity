﻿using System.Collections.Generic;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class DynamicAnimationBehaviorConverter : IDiscoverableBehaviorConverter
    {
        private readonly IDynamicAnimationBehaviorFactory _dynamicAnimationBehaviorFactory;

        public DynamicAnimationBehaviorConverter(IDynamicAnimationBehaviorFactory dynamicAnimationBehaviorFactory)
        {
            _dynamicAnimationBehaviorFactory = dynamicAnimationBehaviorFactory;
        }

        public bool CanConvert(IBehavior behavior) => behavior is IReadOnlyDynamicAnimationBehavior;

        public bool CanConvert(Component component) => component is DynamicAnimationBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IReadOnlyDynamicAnimationBehavior)behavior;
            var component = target.AddComponent<DynamicAnimationBehaviour>();
            component.AnimationId = castedBehavior.BaseAnimationId?.ToString();
            component.Visible = castedBehavior.Visible;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedBehaviour = (DynamicAnimationBehaviour)component;
            var behavior = _dynamicAnimationBehaviorFactory.Create(
                new StringIdentifier(castedBehaviour.AnimationId ?? string.Empty),
                castedBehaviour.Visible,
                0);
            yield return behavior;
        }
    }
}