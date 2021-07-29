using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.GameObjects.Actors.Interactions;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerInteractionControlsBehaviourStitcher : IPlayerInteractionControlsBehaviourStitcher
    {
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IActorInteractionManager _actorInteractionManager;

        public PlayerInteractionControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            IDebugConsoleManager debugConsoleManager,
            IActorInteractionManager actorInteractionManager)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _debugConsoleManager = debugConsoleManager;
            _actorInteractionManager = actorInteractionManager;
        }

        public void Attach(GameObject gameObject)
        {
            var playerInteractionControlsBehaviour = gameObject.AddComponent<PlayerInteractionControlsBehaviour>();
            playerInteractionControlsBehaviour.KeyboardControls = _keyboardControls;
            playerInteractionControlsBehaviour.KeyboardInput = _keyboardInput;
            playerInteractionControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerInteractionControlsBehaviour.ActorInteractionManager = _actorInteractionManager;
            playerInteractionControlsBehaviour.Actor = gameObject.GetGameObject();
        }
    }
}