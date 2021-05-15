
using Macerus.Api.Behaviors;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class TriggerOnCombatEndBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is ITriggerOnCombatEndBehavior;

        public bool CanConvert(Component component) => component is TriggerOnCombatEndBehaviour;

        public Component Convert(
            GameObject target,
            IBehavior behavior)
        {
            var component = target.AddComponent<TriggerOnCombatEndBehaviour>();
            return component;
        }

        public IBehavior Convert(Component component)
        {
            var behavior = new TriggerOnCombatEndBehavior();
            return behavior;
        }
    }
}
