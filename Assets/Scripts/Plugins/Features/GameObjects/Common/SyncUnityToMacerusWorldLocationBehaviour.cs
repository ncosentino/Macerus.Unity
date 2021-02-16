using System;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    /// <summary>
    /// Responsible for synchronizing unity-controlled world location
    /// to the backend.
    /// </summary>
    public sealed class SyncUnityToMacerusWorldLocationBehaviour :
        MonoBehaviour,
        ISyncUnityToMacerusWorldLocationBehaviour
    {
        public IWorldLocationBehavior WorldLocationBehavior { get; set; }

        public IMacerusToUnityWorldLocationSynchronizer MacerusToUnityWorldLocationSynchronizer { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                MacerusToUnityWorldLocationSynchronizer,
                $"{nameof(MacerusToUnityWorldLocationSynchronizer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                MacerusToUnityWorldLocationSynchronizer,
                $"{nameof(WorldLocationBehavior)} was not set on '{gameObject}.{this}'.");

            // sync macerus (source of truth) to unity
            MacerusToUnityWorldLocationSynchronizer.SynchronizeMacerusToUnityWorldLocation(
                gameObject,
                WorldLocationBehavior);
        }

        private void Update()
        {
            SyncUnityToMacerusWorldLocation();
        }

        private void SyncUnityToMacerusWorldLocation()
        {
            WorldLocationBehavior.SetLocation(
                gameObject.transform.position.x,
                gameObject.transform.position.y);
        }
    }
}