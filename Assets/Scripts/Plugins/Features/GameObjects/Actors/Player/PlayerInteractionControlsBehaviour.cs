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
    public sealed class PlayerInteractionControlsBehaviour : MonoBehaviour
    {
        private readonly UnityAsyncRunner _runner = new UnityAsyncRunner();

        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IReadOnlyPlayerInteractionDetectionBehavior PlayerInteractionDetectionBehavior { get; set; }

        public IInteractionHandlerFacade InteractionHandler { get; set; }

        public IActorActionCheck ActorActionCheck { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            this.RequiresNotNull(KeyboardControls, nameof(KeyboardControls));
            this.RequiresNotNull(KeyboardInput, nameof(KeyboardInput));
            this.RequiresNotNull(PlayerInteractionDetectionBehavior, nameof(PlayerInteractionDetectionBehavior));
            this.RequiresNotNull(Logger, nameof(Logger));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(InteractionHandler, nameof(InteractionHandler));
            this.RequiresNotNull(ActorActionCheck, nameof(ActorActionCheck));
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
                var actor = gameObject
                    .GetComponent<IReadOnlyHasGameObject>()
                    .GameObject;
                if (!ActorActionCheck.CanAct(actor))
                {
                    return;
                }

                var interactable = PlayerInteractionDetectionBehavior
                    .Interactables
                    .FirstOrDefault();
                if (interactable != null)
                {
                    await InteractionHandler
                        .InteractAsync(actor, interactable)
                        .ConfigureAwait(false);
                }
            }
        }
    }
}
