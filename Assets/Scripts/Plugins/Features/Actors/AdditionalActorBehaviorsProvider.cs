using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public sealed class AdditionalActorBehaviorsProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors()
        {
            yield return new TestInjectedActorBehavior();
        }
    }
}