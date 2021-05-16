using NexusLabs.Contracts;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

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
            UnityContracts.RequiresNotNull(this, MacerusToUnityPositionSynchronizer, nameof(MacerusToUnityPositionSynchronizer));
            UnityContracts.RequiresNotNull(this, PositionBehavior, nameof(PositionBehavior));

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