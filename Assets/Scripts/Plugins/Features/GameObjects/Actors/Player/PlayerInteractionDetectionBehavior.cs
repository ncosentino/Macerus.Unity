using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Plugins.Features.Audio.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;

using Macerus.Api.Behaviors;
using ProjectXyz.Api.Behaviors;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionDetectionBehavior :
        MonoBehaviour,
        IObservablePlayerInteractionDetectionBehavior
    {
        private readonly List<GameObject> _interactables;

        public PlayerInteractionDetectionBehavior()
        {
            _interactables = new List<GameObject>();
        }

        public event EventHandler<EventArgs> NewInteractable;

        public IReadOnlyCollection<IInteractableBehavior> Interactables => _interactables
            .SelectMany(x => x.Get<IInteractableBehavior>())
            .ToArray();

        private void OnTriggerEnter2D(Collider2D collision)
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
                interactionBehavior.Interact(actor);
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
