using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Api.Behaviors;
using Macerus.Game.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers
{
    public sealed class ContainerInterceptor : IDiscoverableGameObjectBehaviorInterceptor
    {
        private static readonly IIdentifier CONTAINER_TYPE_ID = new StringIdentifier("container");

        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IGameObjectManager _gameObjectManager;

        public ContainerInterceptor(
            IObjectDestroyer objectDestroyer,
            IGameObjectManager gameObjectManager)
        {
            _objectDestroyer = objectDestroyer;
            _gameObjectManager = gameObjectManager;
        }

        public void Intercept(
            IGameObject gameObject,
            GameObject unityGameObject)
        {
            var isContainer = gameObject
                .Get<ITypeIdentifierBehavior>()
                .SingleOrDefault()
                ?.TypeId
                ?.Equals(CONTAINER_TYPE_ID) == true;
            if (!isContainer)
            {
                return;
            }

            var containerInteractionBehaviour = unityGameObject
                .AddComponent<ContainerInteractionBehaviour>();
            containerInteractionBehaviour.GameObjectManager = _gameObjectManager;
            containerInteractionBehaviour.ObjectDestroyer = _objectDestroyer;
        }
    }
}
