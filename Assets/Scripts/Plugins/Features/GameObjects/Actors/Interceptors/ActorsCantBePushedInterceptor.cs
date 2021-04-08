using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Combat.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class ActorsCantBePushedInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly ICombatTurnManager _combatTurnManager;

        public ActorsCantBePushedInterceptor(IActorIdentifiers actorIdentifiers, ICombatTurnManager combatTurnManager)
        {
            _actorIdentifiers = actorIdentifiers;
            _combatTurnManager = combatTurnManager;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            if (!gameObject.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(_actorIdentifiers.ActorTypeIdentifier))
            {
                return;
            }

            var cantBePushedBehaviour = unityGameObject.AddComponent<CantBePushedBehaviour>();
            cantBePushedBehaviour.CombatTurnManager = _combatTurnManager;
            cantBePushedBehaviour.RigidBody = unityGameObject.GetComponent<Rigidbody2D>();
        }
    }
}