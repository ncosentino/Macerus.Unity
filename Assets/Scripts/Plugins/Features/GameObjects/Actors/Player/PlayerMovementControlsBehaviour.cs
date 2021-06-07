using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviour : MonoBehaviour
    {
        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IGameObject Actor { get; set; }

        public IMouseMovementHandler MouseMovementHandler { get; set; }
        
        public IKeyboardMovementHandler KeyboardMovementHandler { get; set; }

        private void Start()
        {
            this.RequiresNotNull(Actor, nameof(Actor));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(KeyboardMovementHandler, nameof(KeyboardMovementHandler));
            this.RequiresNotNull(MouseMovementHandler, nameof(MouseMovementHandler));
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

            MouseMovementHandler.HandleMouseMovement(Actor);
            KeyboardMovementHandler.HandleKeyboardMovement(Actor);
        }
    }
}
