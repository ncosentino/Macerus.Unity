using Assets.Scripts.Api.Scenes.Explore;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.GameObjects;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
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
                .GetRequiredComponent<IReadOnlyHasGameObject>()
                .GameObject
                .GetOnly<IMovementBehavior>();
        }
    }
}