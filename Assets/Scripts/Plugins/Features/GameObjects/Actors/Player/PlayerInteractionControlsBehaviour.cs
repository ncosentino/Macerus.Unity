using System.Threading.Tasks;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Threading;

using Macerus.Plugins.Features.GameObjects.Actors.Interactions;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionControlsBehaviour : MonoBehaviour
    {
        private readonly UnityAsyncRunner _runner = new UnityAsyncRunner();

        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IActorInteractionManager ActorInteractionManager { get; set; }

        public IGameObject Actor { get; set; }

        private void Start()
        {
            this.RequiresNotNull(KeyboardControls, nameof(KeyboardControls));
            this.RequiresNotNull(KeyboardInput, nameof(KeyboardInput));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(ActorInteractionManager, nameof(ActorInteractionManager));
            this.RequiresNotNull(Actor, nameof(Actor));
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
                await ActorInteractionManager
                    .TryInteractAsync(Actor)
                    .ConfigureAwait(false);
            }
        }
    }
}
