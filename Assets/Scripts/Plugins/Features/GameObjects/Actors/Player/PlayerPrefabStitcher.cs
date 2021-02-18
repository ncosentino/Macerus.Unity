﻿using Assets.Scripts.Api.GameObjects;
using Assets.Scripts.Scenes.Explore.Camera;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerPrefabStitcher : IDiscoverablePrefabSticher
    {
        private readonly IPlayerMovementControlsBehaviourStitcher _playerInputControlsBehaviourStitcher;
        private readonly IPlayerInteractionControlsBehaviourStitcher _playerInteractionControlsBehaviourStitcher;

        public PlayerPrefabStitcher(
            IPlayerMovementControlsBehaviourStitcher playerInputControlsBehaviourStitcher,
            IPlayerInteractionControlsBehaviourStitcher playerInteractionControlsBehaviourStitcher)
        {
            _playerInputControlsBehaviourStitcher = playerInputControlsBehaviourStitcher;
            _playerInteractionControlsBehaviourStitcher = playerInteractionControlsBehaviourStitcher;
        }

        public IIdentifier PrefabResourceId { get; } = new StringIdentifier(@"Mapping/Prefabs/PlayerPlaceholder");

        public void Stitch(
            GameObject gameObject,
            IIdentifier prefabResourceId)
        {
            gameObject.AddComponent<CameraTargetBehaviour>();
            gameObject.AddComponent<PlayerInteractionDetectionBehavior>();
            _playerInputControlsBehaviourStitcher.Attach(gameObject);
            _playerInteractionControlsBehaviourStitcher.Attach(gameObject);
        }
    }
}