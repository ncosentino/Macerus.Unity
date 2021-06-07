using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerMovementControlsBehaviourStitcher : IPlayerMovementControlsBehaviourStitcher
    {
        private readonly IMouseMovementHandler _mouseMovementHandler;
        private readonly IKeyboardMovementHandler _keyboardMovementHandler;
        private readonly IDebugConsoleManager _debugConsoleManager;

        public PlayerMovementControlsBehaviourStitcher(
            IMouseMovementHandler mouseMovementHandler,
            IKeyboardMovementHandler keyboardMovementHandler,
            IDebugConsoleManager debugConsoleManager)
        {
            _mouseMovementHandler = mouseMovementHandler;
            _keyboardMovementHandler = keyboardMovementHandler;
            _debugConsoleManager = debugConsoleManager;
        }

        public void Attach(GameObject unityGameObject, IGameObject gameObject)
        {
            var playerInputControlsBehaviour = unityGameObject.AddComponent<PlayerMovementControlsBehaviour>();
            playerInputControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerInputControlsBehaviour.MouseMovementHandler = _mouseMovementHandler;
            playerInputControlsBehaviour.KeyboardMovementHandler = _keyboardMovementHandler;
            playerInputControlsBehaviour.Actor = gameObject;
        }
    }
}