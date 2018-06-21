using Assets.Scripts.Plugins.Features.Actors.UnityBehaviours;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public sealed class PlayerPrefabStitcher
    {
        private readonly IPlayerInputControlsBehaviourStitcher _playerInputControlsBehaviourStitcher;
        private readonly IHasGuiInventoryBehaviourStitcher _hasGuiInventoryBehaviourStitcher;

        public PlayerPrefabStitcher(
            IPlayerInputControlsBehaviourStitcher playerInputControlsBehaviourStitcher,
            IHasGuiInventoryBehaviourStitcher hasGuiInventoryBehaviourStitcher)
        {
            _playerInputControlsBehaviourStitcher = playerInputControlsBehaviourStitcher;
            _hasGuiInventoryBehaviourStitcher = hasGuiInventoryBehaviourStitcher;
        }

        public void Stitch(
            GameObject gameObject,
            string prefabResourceId)
        {
            _playerInputControlsBehaviourStitcher.Attach(gameObject);
            _hasGuiInventoryBehaviourStitcher.Attach(gameObject);
        }
    }
}
