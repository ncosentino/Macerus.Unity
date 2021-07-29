using Macerus.Plugins.Features.GameObjects.Actors.Interactions;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionDetectionBehaviourStitcher : IPlayerInteractionDetectionBehaviourStitcher
    {
        private readonly IActorInteractionManager _actorInteractionManager;

        public PlayerInteractionDetectionBehaviourStitcher(IActorInteractionManager actorInteractionManager)
        {
            _actorInteractionManager = actorInteractionManager;
        }

        public void Stitch(GameObject unityGameObject)
        {
            var playerInteractionDetectionBehaviour = unityGameObject.AddComponent<PlayerInteractionDetectionBehaviour>();
            playerInteractionDetectionBehaviour.ActorInteractionManager = _actorInteractionManager;
            playerInteractionDetectionBehaviour.Actor = unityGameObject.GetGameObject();
        }
    }
}
