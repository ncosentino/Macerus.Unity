using System.Threading.Tasks;

using Assets.Scripts.Unity.Threading;

using Macerus.Plugins.Features.GameObjects.Actors.Interactions;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionDetectionBehaviour : MonoBehaviour
    {
        private readonly UnityAsyncRunner _runner;

        public PlayerInteractionDetectionBehaviour()
        {
            _runner = new UnityAsyncRunner();
        }

        public IGameObject Actor { get; set; }

        public IActorInteractionManager ActorInteractionManager { get; set; }

        private void Start()
        {
            this.RequiresNotNull(Actor, nameof(Actor));
            this.RequiresNotNull(ActorInteractionManager, nameof(ActorInteractionManager));
        }

        private async Task OnTriggerEnter2D(Collider2D collision)
        {
            await _runner
                .RunAsync(() => HandleTriggerEnterAsync(collision))
                .ConfigureAwait(false);
        }

        private async Task HandleTriggerEnterAsync(Collider2D collision)
        {
            var collidedObject = collision.gameObject;
            await ActorInteractionManager
                .ObjectEnterInteractionRadiusAsync(
                    Actor,
                    collidedObject.GetGameObject())
                .ConfigureAwait(false);
        }

        private async Task OnTriggerExit2D(Collider2D collision)
        {
            await _runner
                .RunAsync(() => HandleTriggerExitAsync(collision))
                .ConfigureAwait(false);      
        }

        private async Task HandleTriggerExitAsync(Collider2D collision)
        {
            var collidedObject = collision.gameObject;
            await ActorInteractionManager
                .ObjectExitInteractionRadiusAsync(
                    Actor,
                    collidedObject.GetGameObject())
                .ConfigureAwait(false);
        }
    }
}
