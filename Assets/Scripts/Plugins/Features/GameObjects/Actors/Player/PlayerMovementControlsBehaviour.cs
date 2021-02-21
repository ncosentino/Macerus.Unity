using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
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
        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IMovementBehavior MovementBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, KeyboardControls, nameof(KeyboardControls));
            UnityContracts.RequiresNotNull(this, KeyboardInput, nameof(KeyboardInput));
            UnityContracts.RequiresNotNull(this, MovementBehavior, nameof(MovementBehavior));
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, DebugConsoleManager, nameof(DebugConsoleManager));
        }

        private void Update()
        {
            HandleMovementControls();
        }

        private void HandleMovementControls()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

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
