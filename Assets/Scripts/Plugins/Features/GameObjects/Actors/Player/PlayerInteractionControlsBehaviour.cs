using System.Linq;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionControlsBehaviour :
        MonoBehaviour,
        IPlayerInteractionControlsBehaviour
    {
        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IReadOnlyPlayerInteractionDetectionBehavior PlayerInteractionDetectionBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, KeyboardControls, nameof(KeyboardControls));
            UnityContracts.RequiresNotNull(this, KeyboardInput, nameof(KeyboardInput));
            UnityContracts.RequiresNotNull(this, PlayerInteractionDetectionBehavior, nameof(PlayerInteractionDetectionBehavior));
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, DebugConsoleManager, nameof(DebugConsoleManager));
        }

        private void Update()
        {
            HandleInteractionControls();
        }

        private void HandleInteractionControls()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (KeyboardInput.GetKeyUp(KeyboardControls.Interact))
            {
                var interactable = PlayerInteractionDetectionBehavior
                    .Interactables
                    .FirstOrDefault();
                if (interactable != null)
                {
                    var actor = gameObject
                        .GetComponent<IReadOnlyHasGameObject>()
                        .GameObject;
                    interactable.Interact(actor);
                }
            }
        }
    }
}
