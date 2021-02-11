using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Shared.GameObjects;

using Macerus.Api.Behaviors;
using Macerus.Api.GameObjects;
using Macerus.Shared.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static
{
    public sealed class StaticGameObjectRepository : IDiscoverableGameObjectRepository
    {
        private static readonly IIdentifier STATIC_TYPE_ID = new StringIdentifier("static");

        private readonly IBehaviorCollectionFactory _behaviorCollectionFactory;

        public StaticGameObjectRepository(IBehaviorCollectionFactory behaviorCollectionFactory)
        {
            _behaviorCollectionFactory = behaviorCollectionFactory;
        }

        public bool CanCreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId)
        {
            var canCreateFromTemplate = typeId.Equals(STATIC_TYPE_ID) && templateId is StringIdentifier;
            return canCreateFromTemplate;
        }

        public bool CanLoad(
            IIdentifier typeId,
            IIdentifier objectId) => false;

        public IGameObject CreateFromTemplate(
            IIdentifier typeId,
            IIdentifier templateId,
            IReadOnlyDictionary<string, object> properties)
        {
            var staticGameObject = new StaticGameObject(
                new IdentifierBehavior()
                {
                    Id = new StringIdentifier($"{typeId}-{templateId}-{Guid.NewGuid()}"),
                },
                new HasPrefabResourceBehavior()
                {
                    PrefabResourceId = $"Mapping/Prefabs/{typeId}/{templateId}",
                },
                new WorldLocationBehavior()
                {
                    X = Convert.ToDouble(properties["X"], CultureInfo.InvariantCulture),
                    Y = Convert.ToDouble(properties["Y"], CultureInfo.InvariantCulture),
                    Width = Convert.ToDouble(properties["Width"], CultureInfo.InvariantCulture),
                    Height = Convert.ToDouble(properties["Height"], CultureInfo.InvariantCulture),
                },
                //, FIXME: put a size behavior here
                _behaviorCollectionFactory);
            return staticGameObject;
        }

        public IGameObject Load(
            IIdentifier typeId,
            IIdentifier objectId)
        {
            throw new NotSupportedException(
                $"'{GetType()}' does not support '{nameof(Load)}'.");
        }
    }

    public sealed class StaticGameObject : IGameObject
    {
        public StaticGameObject(
            IReadOnlyIdentifierBehavior identifierBehavior,
            IReadOnlyPrefabResourceBehavior prefabResourceBehavior,
            IReadOnlyWorldLocationBehavior worldLocationBehavior,
            IBehaviorCollectionFactory behaviorCollectionFactory)
        {
            Behaviors = behaviorCollectionFactory.Create(
                identifierBehavior,
                prefabResourceBehavior,
                worldLocationBehavior);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}
