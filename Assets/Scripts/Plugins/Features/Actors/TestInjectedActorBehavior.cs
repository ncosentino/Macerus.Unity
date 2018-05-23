using ProjectXyz.Api.Behaviors;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public class TestInjectedActorBehavior : IBehavior
    {
        public void RegisteringToOwner(IHasBehaviors owner)
        {
            Debug.Log($"'{this}' registering to '{owner}'...");
            Owner = owner;
        }

        public void RegisteredToOwner(IHasBehaviors owner)
        {
            Debug.Log($"'{this}' registered to '{owner}'.");
        }

        public IHasBehaviors Owner { get; private set; }
    }
}
