using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Threading;

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
        private readonly IDispatcher _dispatcher;

        public ActorsCantBePushedInterceptor(
            IActorIdentifiers actorIdentifiers,
            ICombatTurnManager combatTurnManager,
            IDispatcher dispatcher)
        {
            _actorIdentifiers = actorIdentifiers;
            _combatTurnManager = combatTurnManager;
            _dispatcher = dispatcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            if (!gameObject.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(_actorIdentifiers.ActorTypeIdentifier))
            {
                return;
            }

            // FIXME: set this up as a stitcher?
            var cantBePushedBehaviour = unityGameObject.AddComponent<CantBePushedBehaviour>();
            cantBePushedBehaviour.CombatTurnManager = _combatTurnManager;
            cantBePushedBehaviour.Dispatcher = _dispatcher;
            cantBePushedBehaviour.RigidBody = unityGameObject.GetComponent<Rigidbody2D>();
        }
    }
}