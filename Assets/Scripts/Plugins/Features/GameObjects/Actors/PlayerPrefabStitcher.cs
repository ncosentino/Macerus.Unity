using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;
using Assets.Scripts.Scenes.Explore.Camera;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors
{
    public sealed class PlayerPrefabStitcher : IDiscoverablePrefabSticher
    {
        private readonly IPlayerInputControlsBehaviourStitcher _playerInputControlsBehaviourStitcher;

        public PlayerPrefabStitcher(IPlayerInputControlsBehaviourStitcher playerInputControlsBehaviourStitcher)
        {
            _playerInputControlsBehaviourStitcher = playerInputControlsBehaviourStitcher;
        }

        public IIdentifier PrefabResourceId { get; } = new StringIdentifier(@"Mapping/Prefabs/PlayerPlaceholder");

        public void Stitch(
            GameObject gameObject,
            IIdentifier prefabResourceId)
        {
            _playerInputControlsBehaviourStitcher.Attach(gameObject);
            gameObject.AddComponent<CameraTargetBehaviour>();
            gameObject.AddComponent<PlayerInteractionDetectionBehavior>();
        }
    }
}
