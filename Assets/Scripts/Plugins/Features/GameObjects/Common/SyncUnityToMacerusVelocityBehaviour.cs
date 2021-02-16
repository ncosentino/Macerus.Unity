
using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common
{
    /// <summary>
    /// Responsible for synchronizing unity-controlled velocity for movement 
    /// to the backend.
    /// </summary>
    public sealed class SyncUnityToMacerusVelocityBehaviour :
        MonoBehaviour
    {
        public IMovementBehavior MovementBehavior { get; set; }

        public Rigidbody2D RigidBody2D { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                MovementBehavior,
                $"{nameof(MovementBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                RigidBody2D,
                $"{nameof(RigidBody2D)} was not set on '{gameObject}.{this}'.");

            // sync macerus (source of truth) to unity
            SyncMacerusToUnityVelocity();
        }

        private void Update()
        {
            SyncUnityToMacerusVelocity();
        }

        private void SyncUnityToMacerusVelocity()
        {
            MovementBehavior.SetVelocity(
                RigidBody2D.velocity.x,
                RigidBody2D.velocity.y);
        }

        private void SyncMacerusToUnityVelocity()
        {
            RigidBody2D.velocity = new Vector2(
                (float)MovementBehavior.VelocityX,
                (float)MovementBehavior.VelocityY);
        }
    }
}