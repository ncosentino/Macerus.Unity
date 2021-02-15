using System.Linq;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.Input;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionControlsBehaviour :
        MonoBehaviour,
        IPlayerInteractionControlsBehaviour
    {
        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public IReadOnlyPlayerInteractionDetectionBehavior PlayerInteractionDetectionBehavior { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        private void Start()
        {
            Contract.RequiresNotNull(
                KeyboardControls,
                $"{nameof(KeyboardControls)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                KeyboardInput,
                $"{nameof(KeyboardInput)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                PlayerInteractionDetectionBehavior,
                $"{nameof(PlayerInteractionDetectionBehavior)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                Logger,
                $"{nameof(Logger)} was not set on '{gameObject}.{this}'.");
        }

        private void Update()
        {
            HandleInteractionControls();
        }

        private void HandleInteractionControls()
        {
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
