using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlySyncMacerusToUnityVelocityBehaviour
    {
        IObservableMovementBehavior ObservableMovementBehavior { get; }

        Rigidbody2D RigidBody2D { get; }
    }
}