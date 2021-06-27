using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Scenes.Explore.Camera;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerPrefabStitcher : IDiscoverablePrefabStitcher
    {
        private readonly IPlayerMovementControlsBehaviourStitcher _playerInputControlsBehaviourStitcher;
        private readonly IPlayerInteractionControlsBehaviourStitcher _playerInteractionControlsBehaviourStitcher;
        private readonly IPlayerQuickSlotControlsBehaviourStitcher _playerQuickSlotControlsBehaviourStitcher;
        private readonly IPlayerInteractionDetectionBehaviourStitcher _playerInteractionDetectionBehaviourStitcher;

        public PlayerPrefabStitcher(
            IPlayerMovementControlsBehaviourStitcher playerInputControlsBehaviourStitcher,
            IPlayerInteractionControlsBehaviourStitcher playerInteractionControlsBehaviourStitcher,
            IPlayerQuickSlotControlsBehaviourStitcher playerQuickSlotControlsBehaviourStitcher,
            IPlayerInteractionDetectionBehaviourStitcher playerInteractionDetectionBehaviourStitcher)
        {
            _playerInputControlsBehaviourStitcher = playerInputControlsBehaviourStitcher;
            _playerInteractionControlsBehaviourStitcher = playerInteractionControlsBehaviourStitcher;
            _playerQuickSlotControlsBehaviourStitcher = playerQuickSlotControlsBehaviourStitcher;
            _playerInteractionDetectionBehaviourStitcher = playerInteractionDetectionBehaviourStitcher;
        }

        public IIdentifier PrefabResourceId { get; } = new StringIdentifier(@"Mapping/Prefabs/Actors/PlayerPlaceholder");

        public void Stitch(
            GameObject unityGameObject,
            IGameObject gameObject,
            IIdentifier prefabResourceId)
        {
            unityGameObject.AddComponent<CameraTargetBehaviour>();
            _playerInteractionDetectionBehaviourStitcher.Stitch(unityGameObject);
            _playerInputControlsBehaviourStitcher.Attach(unityGameObject, gameObject);
            _playerInteractionControlsBehaviourStitcher.Attach(unityGameObject);
            _playerQuickSlotControlsBehaviourStitcher.Attach(unityGameObject, gameObject);
        }
    }
}
