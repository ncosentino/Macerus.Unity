using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class TemplateIdentifierBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is ITemplateIdentifierBehavior;

        public bool CanConvert(Component component) => component is TemplateIdentifierBehaviour;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (ITemplateIdentifierBehavior)behavior;
            var component = target.AddComponent<TemplateIdentifierBehaviour>();
            component.TemplateId = castedBehavior.TemplateId.ToString();
            yield return component;
        }

        public IEnumerable<IBehavior> Convert(Component component)
        {
            var castedBehaviour = (TemplateIdentifierBehaviour)component;
            var id = new StringIdentifier(castedBehaviour.TemplateId);
            var behavior = new TemplateIdentifierBehavior(id);
            yield return behavior;
        }
    }
}