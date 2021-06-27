using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.Interactions.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerInteractionControlsBehaviourStitcher : IPlayerInteractionControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly ILogger _logger;
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IInteractionHandlerFacade _interactionHandlerFacade;
        private readonly IActorActionCheck _actorActionCheck;

        public PlayerInteractionControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            ILogger logger,
            IDebugConsoleManager debugConsoleManager,
            IInteractionHandlerFacade interactionHandlerFacade,
            IActorActionCheck actorActionCheck)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _logger = logger;
            _debugConsoleManager = debugConsoleManager;
            _interactionHandlerFacade = interactionHandlerFacade;
            _actorActionCheck = actorActionCheck;
        }

        public void Attach(GameObject gameObject)
        {
            var playerInteractionControlsBehaviour = gameObject.AddComponent<PlayerInteractionControlsBehaviour>();
            playerInteractionControlsBehaviour.Logger = _logger;
            playerInteractionControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInteractionControlsBehaviour.KeyboardInput = _keyboardInput;
            playerInteractionControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerInteractionControlsBehaviour.InteractionHandler = _interactionHandlerFacade;
            playerInteractionControlsBehaviour.ActorActionCheck = _actorActionCheck;
            playerInteractionControlsBehaviour.PlayerInteractionDetectionBehavior = gameObject
                .GetRequiredComponent<IReadOnlyPlayerInteractionDetectionBehavior>();
        }
    }
}