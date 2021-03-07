using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.GameObjects;
using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Containers.Api;

using NexusLabs.Contracts;

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

            var containerProperties = gameObject
                .GetOnly<IReadOnlyContainerPropertiesBehavior>();
            Contract.RequiresNotNull(
                containerProperties,
                $"'{gameObject}' is expected to have behavior " +
                $"'{typeof(IReadOnlyContainerPropertiesBehavior)}' with non-" +
                $"null property collection.");

            var containerPrefab = new ContainerPrefab(unityGameObject);
            containerPrefab.Collision.enabled = containerProperties.Collisions;
        }
    }
}
