using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.Mapping;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    /// <summary>
    /// Responsible for synchronizing unity-controlled world location
    /// to the backend.
    /// </summary>
    public sealed class SyncUnityToMacerusPositionBehaviour : MonoBehaviour
    {
        public IPositionBehavior PositionBehavior { get; set; }

        public IMacerusToUnityPositionSynchronizer MacerusToUnityPositionSynchronizer { get; set; }

        private void Start()
        {
            this.RequiresNotNull(MacerusToUnityPositionSynchronizer, nameof(MacerusToUnityPositionSynchronizer));
            this.RequiresNotNull(PositionBehavior, nameof(PositionBehavior));

            // sync macerus (source of truth) to unity
            MacerusToUnityPositionSynchronizer.SynchronizeMacerusToUnityPosition(
                gameObject,
                PositionBehavior.X,
                PositionBehavior.Y);
        }

        private void Update()
        {
            SyncUnityToMacerusPosition();
        }

        private void SyncUnityToMacerusPosition()
        {
            PositionBehavior.SetPosition(
                gameObject.transform.position.x,
                gameObject.transform.position.y);
        }
    }
}