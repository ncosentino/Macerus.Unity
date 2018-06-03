using Assets.Scripts.Api.Scenes.Explore;
using Assets.Scripts.Scenes.Explore.GameObjects;
using Assets.Scripts.Unity.GameObjects;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerInputControlsBehaviourStitcher : IPlayerInputControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly ILogger _logger;

        public PlayerInputControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            ILogger logger)
        {
            _keyboardControls = keyboardControls;
            _logger = logger;
        }

        public void Attach(GameObject gameObject)
        {
            var playerInputControlsBehaviour = gameObject.AddComponent<PlayerInputControlsBehaviour>();
            playerInputControlsBehaviour.Logger = _logger;
            playerInputControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInputControlsBehaviour.MovementBehavior = gameObject
                .GetRequiredComponent<HasGameObjectBehaviour>()
                .GameObject
                .GetOnly<IMovementBehavior>();
        }
    }
}