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

        private void Start()
        {
            Contract.RequiresNotNull(
                WorldLocationBehavior,
                $"{nameof(WorldLocationBehavior)} was not set on '{gameObject}.{this}'.");
            WorldLocationBehavior.WorldLocationChanged += WorldLocationBehavior_WorldLocationChanged;

            // sync macerus (source of truth) to unity
            SyncMacerusToUnityWorldLocation();
        }

        private void Update()
        {
            SyncUnityToMacerusWorldLocation();
        }

        private void OnDestroy()
        {
            if (WorldLocationBehavior != null)
            {
                WorldLocationBehavior.WorldLocationChanged -= WorldLocationBehavior_WorldLocationChanged;
            }
        }

        private void SyncMacerusToUnityWorldLocation()
        {
            gameObject.transform.position = new Vector3(
                (float)WorldLocationBehavior.X,
                (float)WorldLocationBehavior.Y,
                gameObject.transform.position.z);
            
            gameObject.transform.localScale = new Vector3(
                (float)WorldLocationBehavior.Width,
                (float)WorldLocationBehavior.Height,
                gameObject.transform.localScale.z);
        }

        private void SyncUnityToMacerusWorldLocation()
        {
            WorldLocationBehavior.SetLocation(
                gameObject.transform.position.x,
                gameObject.transform.position.y);
        }

        private void WorldLocationBehavior_WorldLocationChanged(
            object sender,
            EventArgs e) => SyncMacerusToUnityWorldLocation();
    }
}