using Assets.Scripts.Input.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerInteractionControlsBehaviourStitcher : IPlayerInteractionControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly ILogger _logger;

        public PlayerInteractionControlsBehaviourStitcher(
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
            var playerInteractionControlsBehaviour = gameObject.AddComponent<PlayerInteractionControlsBehaviour>();
            playerInteractionControlsBehaviour.Logger = _logger;
            playerInteractionControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInteractionControlsBehaviour.KeyboardInput = _keyboardInput;
            playerInteractionControlsBehaviour.PlayerInteractionDetectionBehavior = gameObject
                .GetRequiredComponent<IReadOnlyPlayerInteractionDetectionBehavior>();
        }
    }
}