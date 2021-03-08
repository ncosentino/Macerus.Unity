using System;

using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;
using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Static.Triggers
{
    public sealed class EncounterTriggerBehaviour : MonoBehaviour
    {
        private DateTime? _lastMovementDetected;
        private TimeSpan _elapsed;

        // FIXME: can this be affected by stats?
        public TimeSpan TriggerInterval { get; set; }

        // FIXME: can this be affected by stats?
        public double ChanceToTrigger { get; set; }

        public bool MustBeMoving { get; set; }

        public IRandom Random { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, Random, nameof(Random));

            _elapsed = TimeSpan.FromSeconds(0);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.gameObject.IsPlayerControlled())
            {
                return;
            }

            var playerUnity = collision.gameObject;
            var player = playerUnity.GetComponent<IHasGameObject>().GameObject;
            if (MustBeMoving)
            {
                var movementBehavior = player.GetOnly<IReadOnlyMovementBehavior>();
                if (movementBehavior.VelocityX == 0 && movementBehavior.VelocityY == 0)
                {
                    return;
                }
            }

            _elapsed += DateTime.UtcNow - _lastMovementDetected.Value;
            _lastMovementDetected = DateTime.UtcNow;

            if (_elapsed < TriggerInterval)
            {
                return;
            }

            _elapsed = TimeSpan.FromSeconds(0);

            if (ChanceToTrigger < Random.NextDouble(0, 1))
            {
                return;
            }

            Debug.Log("FIXME: this was an encounter!");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _lastMovementDetected = DateTime.UtcNow;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _lastMovementDetected = DateTime.UtcNow;
        }
    }
}
