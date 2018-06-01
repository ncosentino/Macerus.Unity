using Assets.Scripts.Plugins.Features.Actors.UnityBehaviours;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors
{
    public sealed class PlayerPrefabStitcher
    {
        private readonly IPlayerInputControlsBehaviourStitcher _playerInputControlsBehaviourStitcher;

        public PlayerPrefabStitcher(IPlayerInputControlsBehaviourStitcher playerInputControlsBehaviourStitcher)
        {
            _playerInputControlsBehaviourStitcher = playerInputControlsBehaviourStitcher;
        }

        public void Stitch(
            GameObject gameObject,
            string prefabResourceId)
        {
            _playerInputControlsBehaviourStitcher.Attach(gameObject);
        }
    }
}
