using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.GameObjects.Skills.Api;

using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class PlayerQuickSlotControlsBehaviourStitcher : IPlayerQuickSlotControlsBehaviourStitcher
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IMapGameObjectManager _mapGameObjectManager;
        private readonly ITurnBasedManager _turnBasedManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IKeyboardInput _keyboardInput;
        private readonly ILogger _logger;
        private readonly ISkillUsage _skillUsage;
        private readonly ISkillHandlerFacade _skillHandlerFacade;

        public PlayerQuickSlotControlsBehaviourStitcher(
            IKeyboardControls keyboardControls,
            IKeyboardInput keyboardInput,
            ILogger logger,
            ISkillUsage skillUsage,
            ISkillHandlerFacade skillHandlerFacade,
            IDebugConsoleManager debugConsoleManager,
            IMapGameObjectManager mapGameObjectManager,
            ITurnBasedManager turnBasedManager)
        {
            _keyboardControls = keyboardControls;
            _keyboardInput = keyboardInput;
            _logger = logger;
            _skillUsage = skillUsage;
            _skillHandlerFacade = skillHandlerFacade;
            _debugConsoleManager = debugConsoleManager;
            _mapGameObjectManager = mapGameObjectManager;
            _turnBasedManager = turnBasedManager;
        }

        public IReadOnlyPlayerQuickSlotControlsBehaviour Attach(GameObject gameObject)
        {
            var playerQuickSlotControlsBehaviour = gameObject.AddComponent<PlayerQuickSlotControlsBehaviour>();
            playerQuickSlotControlsBehaviour.Logger = _logger;
            playerQuickSlotControlsBehaviour.KeyboardControls = _keyboardControls;
            playerQuickSlotControlsBehaviour.KeyboardInput = _keyboardInput;
            playerQuickSlotControlsBehaviour.DebugConsoleManager = _debugConsoleManager;
            playerQuickSlotControlsBehaviour.SkillUsage = _skillUsage;
            playerQuickSlotControlsBehaviour.SkillHandlerFacade = _skillHandlerFacade;
            playerQuickSlotControlsBehaviour.MapGameObjectManager = _mapGameObjectManager;
            playerQuickSlotControlsBehaviour.TurnBasedManager = _turnBasedManager;
            return playerQuickSlotControlsBehaviour;
        }
    }
}
