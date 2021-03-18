using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Shared.GameObjects;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorBehaviorsProvider : IDiscoverableActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            // TODO: pull this information from the back-end game object.
            // translate as necessary (i.e. if it's an ID we need to lookup
            // and translate to a path, so be it)
            if (baseBehaviors.Any(x => x is IPlayerControlledBehavior))
            {
                yield return new HasPrefabResourceBehavior()
                {
                    PrefabResourceId = new StringIdentifier("Mapping/Prefabs/Actors/PlayerPlaceholder"),
                };
            }
            else
            {
                yield return new HasPrefabResourceBehavior()
                {
                    PrefabResourceId = new StringIdentifier("Mapping/Prefabs/Actors/Actor"),
                };
            }
        }
    }
}