using Macerus.Plugins.Features.Interactions.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionDetectionBehaviourStitcher : IPlayerInteractionDetectionBehaviourStitcher
    {
        private readonly IInteractionHandlerFacade _interactionHandler;

        public PlayerInteractionDetectionBehaviourStitcher(IInteractionHandlerFacade interactionHandlerFacade)
        {
            _interactionHandler = interactionHandlerFacade;
        }

        public void Stitch(GameObject unityGameObject)
        {
            var playerInteractionDetectionBehaviour = unityGameObject.AddComponent<PlayerInteractionDetectionBehaviour>();
            playerInteractionDetectionBehaviour.InteractionHandler = _interactionHandler;
        }
    }
}
