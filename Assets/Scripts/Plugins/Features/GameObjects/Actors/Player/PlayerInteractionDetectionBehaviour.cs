using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Assets.Scripts.Plugins.Features.Audio.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.Interactions.Api;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionDetectionBehaviour :
        MonoBehaviour,
        IObservablePlayerInteractionDetectionBehavior
    {
        private readonly UnityAsynRunner _runner;
        private readonly List<GameObject> _interactables;

        public PlayerInteractionDetectionBehaviour()
        {
            _interactables = new List<GameObject>();
            _runner = new UnityAsynRunner();
        }

        public event EventHandler<EventArgs> NewInteractable;

        public IInteractionHandlerFacade InteractionHandler { get; set; }

        public IReadOnlyCollection<IInteractableBehavior> Interactables => _interactables
            .SelectMany(x => x.Get<IInteractableBehavior>())
            .ToArray();

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, InteractionHandler, nameof(InteractionHandler));
        }

        private async void OnTriggerEnter2D(Collider2D collision)
        {
            await _runner.RunAsync(() => HandleTriggerEnterAsync(collision));
        }

        private async Task HandleTriggerEnterAsync(Collider2D collision)
        {
            var collidedObject = collision.gameObject;
            var interactionBehavior = collidedObject
                .Get<IInteractableBehavior>()
                .FirstOrDefault();
            if (interactionBehavior == null)
            {
                return;
            }

            if (interactionBehavior.AutomaticInteraction)
            {
                var actor = gameObject
                    .GetComponent<IReadOnlyHasGameObject>()
                    .GameObject;
                await InteractionHandler
                    .InteractAsync(actor, interactionBehavior)
                    .ConfigureAwait(false);
                return;
            }

            _interactables.Add(collidedObject);

            CheckForNoise(collidedObject);

            NewInteractable?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var collidedObject = collision.gameObject;
            if (!collidedObject.Has<IInteractableBehavior>())
            {
                return;
            }

            _interactables.Remove(collidedObject);
        }

        private void CheckForNoise(GameObject collidedWith)
        {
            var makesNoise = collidedWith.Get<IMakeNoiseBehaviour>().FirstOrDefault();
            if (makesNoise == null)
            {
                return;
            }

            var soundPlayer = collidedWith.GetComponentInParent<ICanPlaySound>();
            if (soundPlayer == null)
            {
                return;
            }

            soundPlayer.PlaySound(makesNoise.GetNoise());
        }
    }
}
