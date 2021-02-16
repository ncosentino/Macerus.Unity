using System;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    /// <summary>
    /// Responsible for synchronizing backend controlled world location
    /// to the front-end Unity.
    /// </summary>
    public sealed class SyncMacerusToUnityWorldLocationBehaviour :
        MonoBehaviour,
        ISyncMacerusToUnityWorldLocationBehaviour
    {
        public IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; set; }

        public IMacerusToUnityWorldLocationSynchronizer MacerusToUnityWorldLocationSynchronizer { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                MacerusToUnityWorldLocationSynchronizer,
                $"{nameof(MacerusToUnityWorldLocationSynchronizer)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                ObservableWorldLocationBehavior,
                $"{nameof(ObservableWorldLocationBehavior)} was not set on '{gameObject}.{this}'.");
            ObservableWorldLocationBehavior.WorldLocationChanged += WorldLocationBehavior_WorldLocationChanged;

            // sync macerus (source of truth) to unity
            MacerusToUnityWorldLocationSynchronizer.SynchronizeMacerusToUnityWorldLocation(
                gameObject,
                ObservableWorldLocationBehavior);
        }

        private void OnDestroy()
        {
            if (ObservableWorldLocationBehavior != null)
            {
                ObservableWorldLocationBehavior.WorldLocationChanged -= WorldLocationBehavior_WorldLocationChanged;
            }
        }

        private void WorldLocationBehavior_WorldLocationChanged(
            object sender,
            EventArgs e) => MacerusToUnityWorldLocationSynchronizer.SynchronizeMacerusToUnityWorldLocation(
                gameObject,
                ObservableWorldLocationBehavior);
    }
}