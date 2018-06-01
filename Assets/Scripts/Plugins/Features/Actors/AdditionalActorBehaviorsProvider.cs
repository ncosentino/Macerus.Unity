﻿using System.Collections.Generic;
using Assets.Scripts.Plugins.Features.Actors.Behaviors;
using Assets.Scripts.Shared.GameObjects;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public sealed class AdditionalActorBehaviorsProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IGameObject gameObject)
        {
            yield return new TestInjectedActorBehavior();
            yield return new HasPrefabResourceBehavior()
            {
                // TODO: pull this information from the back-end game object.
                // translate as necessary (i.e. if it's an ID we need to lookup
                // and translate to a path, so be it)
                PrefabResourceId = "Mapping/Prefabs/PlayerPlaceholder",
            };
        }
    }
}