using Assets.Scripts.Api.Scenes.Explore;
using Macerus.Api.Behaviors;
using ProjectXyz.Framework.Contracts;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public sealed class PlayerInputControlsBehaviour : 
        MonoBehaviour,
        IPlayerInputControlsBehaviour
    {
        public IKeyboardControls KeyboardControls { get; set; }

        public IMovementBehavior MovementBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                KeyboardControls,
                $"{nameof(KeyboardControls)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                MovementBehavior,
                $"{nameof(MovementBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                Logger,
                $"{nameof(Logger)} was not set on '{gameObject}.{this}'.");
        }

        private void Update()
        {
            HandleMovementControls();
        }

        private void HandleMovementControls()
        {
            float throttleY;
            if (Input.GetKey(KeyboardControls.MoveDown))
            {
                throttleY = -1;
            }
            else if (Input.GetKey(KeyboardControls.MoveUp))
            {
                throttleY = 1;
            }
            else
            {
                throttleY = 0;
            }

            float throttleX;
            if (Input.GetKey(KeyboardControls.MoveLeft))
            {
                throttleX = -1;
            }
            else if (Input.GetKey(KeyboardControls.MoveRight))
            {
                throttleX = 1;
            }
            else
            {
                throttleX = 0;
            }

            MovementBehavior.ThrottleX = throttleX;
            MovementBehavior.ThrottleY = throttleY;
        }
    }
}
