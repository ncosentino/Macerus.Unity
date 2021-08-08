using System.Collections.Generic;

using Macerus.Plugins.Features.Encounters.Triggers;
using Macerus.Plugins.Features.Encounters.Default.Triggers;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EncounterTriggerPropertiesBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is IReadOnlyEncounterTriggerPropertiesBehavior;

        public bool CanConvert(Component component) => component is EncounterTriggerPropertiesBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (IReadOnlyEncounterTriggerPropertiesBehavior)behavior;
            var component = target.AddComponent<EncounterTriggerPropertiesBehaviour>();
            component.MustBeMoving = castedBehavior.MustBeMoving;
            component.EncounterId = castedBehavior.EncounterId?.ToString();
            component.IntervalInMilliseconds = ((IInterval<double>)castedBehavior.EncounterInterval).Value;
            component.EncounterChance = castedBehavior.EncounterChance;
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var behaviour = (EncounterTriggerPropertiesBehaviour)component;
            var behavior = new EncounterTriggerPropertiesBehavior(
                behaviour.MustBeMoving,
                string.IsNullOrWhiteSpace(behaviour.EncounterId) ? null : new StringIdentifier(behaviour.EncounterId),
                new Interval<double>(behaviour.IntervalInMilliseconds),
                behaviour.EncounterChance);
            yield return behavior;
        }
    }
}
