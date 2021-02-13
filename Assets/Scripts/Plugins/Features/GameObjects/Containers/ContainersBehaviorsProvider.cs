using System.Collections.Generic;

using Assets.Scripts.Shared.GameObjects;

using Macerus.Plugins.Features.GameObjects.Containers.Api;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public sealed class ContainersBehaviorsProvider : IDiscoverableContainerBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            var typeId = baseBehaviors
                .GetOnly<ITypeIdentifierBehavior>()
                .TypeId;
            var templateId = baseBehaviors
                .GetOnly<ITemplateIdentifierBehavior>()
                .TemplateId;
            yield return new HasPrefabResourceBehavior()
            {
                PrefabResourceId = new StringIdentifier($"Mapping/Prefabs/{typeId}/{templateId}"),
            };
        }
    }
}