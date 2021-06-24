using System.Linq;
using System.Threading.Tasks;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Threading;

using Macerus.Plugins.Features.Interactions.Api;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionControlsBehaviour :
        MonoBehaviour,
        IReadOnlyPlayerInteractionControlsBehaviour
    {
        private readonly UnityAsynRunner _runner = new UnityAsynRunner();

        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IReadOnlyPlayerInteractionDetectionBehavior PlayerInteractionDetectionBehavior { get; set; }

        public IInteractionHandlerFacade InteractionHandler { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, KeyboardControls, nameof(KeyboardControls));
            UnityContracts.RequiresNotNull(this, KeyboardInput, nameof(KeyboardInput));
            UnityContracts.RequiresNotNull(this, PlayerInteractionDetectionBehavior, nameof(PlayerInteractionDetectionBehavior));
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, DebugConsoleManager, nameof(DebugConsoleManager));
            UnityContracts.RequiresNotNull(this, InteractionHandler, nameof(InteractionHandler));
        }

        private async Task Update()
        {
            await _runner.RunAsync(HandleInteractionControlsAsync);
        }

        private async Task HandleInteractionControlsAsync()
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
                    await InteractionHandler
                        .InteractAsync(actor, interactable)
                        .ConfigureAwait(false);
                }
            }
        }
    }
}
