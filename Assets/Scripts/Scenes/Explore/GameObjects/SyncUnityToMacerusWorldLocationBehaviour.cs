using System;

using Macerus.Api.Behaviors;

using ProjectXyz.Framework.Contracts;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
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
                -1);
            
            // FIXME: this is a total hack but...
            var boxCollider = gameObject.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.size = new Vector2(
                    (float)WorldLocationBehavior.Width,
                    (float)WorldLocationBehavior.Height);
            }
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