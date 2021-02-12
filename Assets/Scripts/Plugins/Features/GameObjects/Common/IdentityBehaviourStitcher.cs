using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    public sealed class IdentityBehaviourStitcher : IIdentityBehaviourStitcher
    {
        public void Stitch(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var gameObjectId = gameObject
                .GetOnly<IIdentifierBehavior>()
                .Id;

            // we must have an identifier behaviour on a map game object
            var identifierBehaviour = unityGameObject.GetComponent<IIdentifierBehaviour>();
            if (identifierBehaviour == null)
            {
                identifierBehaviour = unityGameObject.AddComponent<IdentifierBehaviour>();
            }

            identifierBehaviour.Id = gameObjectId;
            unityGameObject.name = $"GameObject {gameObjectId}";
        }
    }
}