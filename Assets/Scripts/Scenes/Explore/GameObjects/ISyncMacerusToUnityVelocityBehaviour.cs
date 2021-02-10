using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface ISyncMacerusToUnityVelocityBehaviour : IReadOnlySyncMacerusToUnityVelocityBehaviour
    {
        new IObservableMovementBehavior ObservableMovementBehavior { get; set; }

        new Rigidbody2D RigidBody2D { get; set; }
    }
}