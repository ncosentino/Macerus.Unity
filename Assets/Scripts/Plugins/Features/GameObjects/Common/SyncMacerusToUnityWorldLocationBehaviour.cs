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

        private void Start()
        {
            Contract.RequiresNotNull(
                ObservableWorldLocationBehavior,
                $"{nameof(ObservableWorldLocationBehavior)} was not set on '{gameObject}.{this}'.");
            ObservableWorldLocationBehavior.WorldLocationChanged += WorldLocationBehavior_WorldLocationChanged;

            // sync macerus (source of truth) to unity
            SyncMacerusToUnityWorldLocation();
        }

        private void OnDestroy()
        {
            if (ObservableWorldLocationBehavior != null)
            {
                ObservableWorldLocationBehavior.WorldLocationChanged -= WorldLocationBehavior_WorldLocationChanged;
            }
        }

        private void SyncMacerusToUnityWorldLocation()
        {
            gameObject.transform.position = new Vector3(
                (float)ObservableWorldLocationBehavior.X,
                (float)ObservableWorldLocationBehavior.Y,
                gameObject.transform.position.z);

            gameObject.transform.localScale = new Vector3(
                (float)ObservableWorldLocationBehavior.Width,
                (float)ObservableWorldLocationBehavior.Height,
                gameObject.transform.localScale.z);
        }

        private void WorldLocationBehavior_WorldLocationChanged(
            object sender,
            EventArgs e) => SyncMacerusToUnityWorldLocation();
    }
}