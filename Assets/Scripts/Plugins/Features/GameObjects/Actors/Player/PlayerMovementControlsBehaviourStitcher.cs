using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using Macerus.Plugins.Features.GameObjects.Actors.Interactions;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviourStitcher : IPlayerMovementControlsBehaviourStitcher
    {
        private readonly IMouseMovementHandler _mouseMovementHandler;
        private readonly IKeyboardMovementHandler _keyboardMovementHandler;
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IActorActionCheck _actorActionCheck;

        public PlayerMovementControlsBehaviourStitcher(
            IMouseMovementHandler mouseMovementHandler,
            IKeyboardMovementHandler keyboardMovementHandler,
            IDebugConsoleManager debugConsoleManager,
            IActorActionCheck actorActionCheck)
        {
            _mouseMovementHandler = mouseMovementHandler;
            _keyboardMovementHandler = keyboardMovementHandler;
            _debugConsoleManager = debugConsoleManager;
            _actorActionCheck = actorActionCheck;
        }

        public void Attach(GameObject unityGameObject, IGameObject gameObject)
        {
            var playerInputControlsBehaviour = unityGameObject.AddComponent<PlayerMovementControlsBehaviour>();
            playerInputControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerInputControlsBehaviour.MouseMovementHandler = _mouseMovementHandler;
            playerInputControlsBehaviour.KeyboardMovementHandler = _keyboardMovementHandler;
            playerInputControlsBehaviour.ActorActionCheck = _actorActionCheck;
            playerInputControlsBehaviour.Actor = gameObject;
        }
    }
}