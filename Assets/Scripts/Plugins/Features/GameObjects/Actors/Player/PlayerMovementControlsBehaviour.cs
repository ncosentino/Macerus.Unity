using System.Threading.Tasks;

using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Threading;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviour : MonoBehaviour
    {
        private readonly UnityAsyncRunner _unityAsyncRunner = new UnityAsyncRunner();

        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IGameObject Actor { get; set; }

        public IMouseMovementHandler MouseMovementHandler { get; set; }
        
        public IKeyboardMovementHandler KeyboardMovementHandler { get; set; }

        public IActorActionCheck ActorActionCheck { get; set; }

        private void Start()
        {
            this.RequiresNotNull(Actor, nameof(Actor));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(KeyboardMovementHandler, nameof(KeyboardMovementHandler));
            this.RequiresNotNull(MouseMovementHandler, nameof(MouseMovementHandler));
            this.RequiresNotNull(ActorActionCheck, nameof(ActorActionCheck));
        }

        private async Task Update()
        {
            await _unityAsyncRunner.RunAsync(HandleMovementControlsAsync);
        }

        private async Task HandleMovementControlsAsync()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (!ActorActionCheck.CanAct(Actor))
            {
                return;
            }

            await MouseMovementHandler
                .HandleMouseMovementAsync(Actor)
                .ConfigureAwait(true); // we need this to resume back on the main thread
            KeyboardMovementHandler.HandleKeyboardMovement(Actor);
        }
    }
}
