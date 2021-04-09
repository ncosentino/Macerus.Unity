using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Interceptors
{
    public sealed class DrawActorWalkPathInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private readonly IActorIdentifiers _actorIdentifiers;
        private readonly IDrawWalkPathBehaviourStitcher _drawWalkPathBehaviourStitcher;

        public DrawActorWalkPathInterceptor(
            IActorIdentifiers actorIdentifiers,
            IDrawWalkPathBehaviourStitcher drawWalkPathBehaviourStitcher)
        {
            _actorIdentifiers = actorIdentifiers;
            _drawWalkPathBehaviourStitcher = drawWalkPathBehaviourStitcher;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            if (!gameObject.GetOnly<ITypeIdentifierBehavior>().TypeId.Equals(_actorIdentifiers.ActorTypeIdentifier))
            {
                return;
            }

            var drawWalkPathBehaviour = _drawWalkPathBehaviourStitcher.Stitch(
                gameObject,
                unityGameObject);

            // FIXME: maybe change the color for different teams?
        }
    }
}