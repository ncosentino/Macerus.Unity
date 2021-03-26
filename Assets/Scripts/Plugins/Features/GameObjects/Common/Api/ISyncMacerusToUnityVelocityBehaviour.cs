using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface ISyncMacerusToUnityVelocityBehaviour : IReadOnlySyncMacerusToUnityVelocityBehaviour
    {
        new IObservableMovementBehavior ObservableMovementBehavior { get; set; }

        new Rigidbody2D RigidBody2D { get; set; }

        new IDispatcher Dispatcher { get; set; }
    }
}