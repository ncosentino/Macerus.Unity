using Assets.Scripts;
using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using Macerus.Api.Behaviors;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerMovementControlsBehaviourStitcher : IPlayerMovementControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly ILogger _logger;

        public PlayerMovementControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            ILogger logger)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _logger = logger;
        }

        public void Attach(GameObject gameObject)
        {
            var playerInputControlsBehaviour = gameObject.AddComponent<PlayerMovementControlsBehaviour>();
            playerInputControlsBehaviour.Logger = _logger;
            playerInputControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInputControlsBehaviour.KeyboardInput = _keyboardInput;
            playerInputControlsBehaviour.MovementBehavior = gameObject
                .GetRequiredComponent<IReadOnlyHasGameObject>()
                .GameObject
                .GetOnly<IMovementBehavior>();
        }
    }
}