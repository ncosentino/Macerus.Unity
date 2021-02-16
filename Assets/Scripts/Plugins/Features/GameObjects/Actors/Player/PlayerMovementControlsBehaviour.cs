using Assets.Scripts.Input.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviour :
        MonoBehaviour,
        IPlayerMovementControlsBehaviour
    {
        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IMovementBehavior MovementBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                KeyboardControls,
                $"{nameof(KeyboardControls)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                KeyboardInput,
                $"{nameof(KeyboardInput)} was not set on '{gameObject}.{this}'.");
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
            if (KeyboardInput.GetKey(KeyboardControls.MoveDown))
            {
                throttleY = -1;
            }
            else if (KeyboardInput.GetKey(KeyboardControls.MoveUp))
            {
                throttleY = 1;
            }
            else
            {
                throttleY = 0;
            }

            float throttleX;
            if (KeyboardInput.GetKey(KeyboardControls.MoveLeft))
            {
                throttleX = -1;
            }
            else if (KeyboardInput.GetKey(KeyboardControls.MoveRight))
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
