using Assets.Scripts.Api.GameObjects;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
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