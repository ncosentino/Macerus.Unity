using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IReadOnlySyncMacerusToUnityVelocityBehaviour
    {
        IObservableMovementBehavior ObservableMovementBehavior { get; }

        Rigidbody2D RigidBody2D { get; }

        IDispatcher Dispatcher { get; }
    }
}