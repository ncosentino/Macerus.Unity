using System;
using System.Collections.Generic;

using Macerus.Api.Behaviors;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public sealed class PlayerInteractionDetectionBehavior :
        MonoBehaviour
    {
        private readonly List<GameObject> _interactables;

        public PlayerInteractionDetectionBehavior()
        {
            _interactables = new List<GameObject>();
        }

        public event EventHandler<EventArgs> NewInteractable;

        public IReadOnlyCollection<GameObject> Interactables => _interactables;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var collidedObject = collision.gameObject;
            if (!collidedObject.Has<IInteractableBehavior>())
            {
                return;
            }

            _interactables.Add(collidedObject);
            Debug.Log($"'{gameObject}' can interact with '{collidedObject}'.");

            NewInteractable?.Invoke(
                this,
                EventArgs.Empty);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            var collidedObject = collision.gameObject;
            if (!collidedObject.Has<IInteractableBehavior>())
            {
                return;
            }

            _interactables.Remove(collidedObject);
            Debug.Log(
                $"'{gameObject}' can no longer interact with '{collidedObject}'.");
        }
    }
}
