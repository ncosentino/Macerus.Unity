using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Threading;

using Macerus.Api.Behaviors;
using Macerus.Plugins.Features.GameObjects.Actors.Interactions;
using Macerus.Plugins.Features.StatusBar.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerQuickSlotControlsBehaviour : MonoBehaviour
    {
        private readonly UnityAsyncRunner _runner = new UnityAsyncRunner();
        private readonly Dictionary<KeyCode, int> _keyToSlotIndex;

        private int? _primedSkillIndex;
        private System.Numerics.Vector2? _lastMouseTargetPosition;
        private int? _lastDirection;

        public PlayerQuickSlotControlsBehaviour()
        {
            _keyToSlotIndex = new Dictionary<KeyCode, int>();
        }

        public IDebugConsoleManager DebugConsoleManager { get; set; }

        public IKeyboardControls KeyboardControls { get; set; }

        public IKeyboardInput KeyboardInput { get; set; }

        public ProjectXyz.Api.Logging.ILogger Logger { get; set; }

        public IStatusBarController StatusBarController { get; set; }

        public IGameObject Actor { get; set; }

        public IActorActionCheck ActorActionCheck { get; set; }

        public IMouseInput MouseInput { get; set; }

        public IScreenPointToMapCellConverter ScreenPointToMapCellConverter { get; set; }

        private void Start()
        {
            this.RequiresNotNull(Logger, nameof(Logger));
            this.RequiresNotNull(KeyboardInput, nameof(KeyboardInput));
            this.RequiresNotNull(KeyboardControls, nameof(KeyboardControls));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(StatusBarController, nameof(StatusBarController));
            this.RequiresNotNull(ActorActionCheck, nameof(ActorActionCheck));
            this.RequiresNotNull(Actor, nameof(Actor));
            this.RequiresNotNull(MouseInput, nameof(MouseInput));
            this.RequiresNotNull(ScreenPointToMapCellConverter, nameof(ScreenPointToMapCellConverter));

            _keyToSlotIndex[KeyboardControls.QuickSlot1] = 0;
            _keyToSlotIndex[KeyboardControls.QuickSlot2] = 1;
            _keyToSlotIndex[KeyboardControls.QuickSlot3] = 2;
            _keyToSlotIndex[KeyboardControls.QuickSlot4] = 3;
            _keyToSlotIndex[KeyboardControls.QuickSlot5] = 4;
            _keyToSlotIndex[KeyboardControls.QuickSlot6] = 5;
            _keyToSlotIndex[KeyboardControls.QuickSlot7] = 6;
            _keyToSlotIndex[KeyboardControls.QuickSlot8] = 7;
            _keyToSlotIndex[KeyboardControls.QuickSlot9] = 8;
            _keyToSlotIndex[KeyboardControls.QuickSlot10] = 9;

            Actor.GetOnly<IObservablePositionBehavior>().PositionChanged += PlayerQuickSlotControlsBehaviour_PositionChanged;
        }

        private void OnDestroy()
        {
            Actor.GetOnly<IObservablePositionBehavior>().PositionChanged -= PlayerQuickSlotControlsBehaviour_PositionChanged;
        }

        private async Task Update()
        {
            await _runner.RunAsync(HandleQuickSlotControlsAsync);
        }

        private async Task UsePrimedSkillAsync()
        {
            var skillSlotIndex = _primedSkillIndex.Value;
            _primedSkillIndex = null;
            _lastMouseTargetPosition = null;
            _lastDirection = null;
            await StatusBarController
                .ActivateSkillSlotAsync(
                    Actor,
                    skillSlotIndex)
                .ConfigureAwait(false);
        }

        private async Task HandleQuickSlotControlsAsync()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (!ActorActionCheck.CanAct(Actor))
            {
                return;
            }

            if (MouseInput.GetMouseButtonUp(0) && _primedSkillIndex.HasValue)
            {
                await UsePrimedSkillAsync().ConfigureAwait(false);
            }

            foreach (var entry in _keyToSlotIndex.OrderBy(x => x.Value))
            {
                if (!KeyboardInput.GetKeyUp(entry.Key))
                {
                    continue;
                }

                if (_primedSkillIndex.HasValue)
                {
                    await UsePrimedSkillAsync().ConfigureAwait(false);
                }
                else
                {
                    _primedSkillIndex = entry.Value;
                    _lastMouseTargetPosition = null;
                    _lastDirection = null;
                    await StatusBarController
                        .ClearSkillSlotPreviewAsync()
                        .ConfigureAwait(false);
                }

                break;
            }

            if (_primedSkillIndex.HasValue)
            {
                var movementBehavior = Actor.GetOnly<IMovementBehavior>();

                var currentMouseTargetPosition = GetTilePositionForCursor();
                if (!_lastMouseTargetPosition.HasValue ||
                    currentMouseTargetPosition != _lastMouseTargetPosition.Value)
                {
                    _lastMouseTargetPosition = currentMouseTargetPosition;

                    // make the actor face the direction of the cursor
                    var positionBehavior = Actor.GetOnly<IReadOnlyPositionBehavior>();
                    var direction = new System.Numerics.Vector2(
                        (float)(_lastMouseTargetPosition.Value.X - positionBehavior.X),
                        (float)(_lastMouseTargetPosition.Value.Y - positionBehavior.Y));
                    movementBehavior.SetDirectionByVector(direction);
                    _lastDirection = null;
                }

                if (!_lastDirection.HasValue ||
                    _lastDirection != movementBehavior.Direction)
                {
                    _lastDirection = movementBehavior.Direction;
                    await StatusBarController
                        .PreviewSkillSlotAsync(
                            Actor,
                            _primedSkillIndex.Value)
                        .ConfigureAwait(false);
                }
            }
        }

        private System.Numerics.Vector2 GetTilePositionForCursor()
        {
            var mapCell = ScreenPointToMapCellConverter.Convert(MouseInput.Position);
            // FIXME: no idea why only x has to be adjusted here... seems
            // SO wrong but it makes it work. otherwise, the y coordinate
            // of the hover shows up right but it's off-by-one for x.
            var correctedForTileCenter = new System.Numerics.Vector2(mapCell.x - 0.5f, mapCell.y);
            return correctedForTileCenter;
        }

        private void PlayerQuickSlotControlsBehaviour_PositionChanged(
            object sender,
            EventArgs e)
        {
            _primedSkillIndex = null;
            _lastDirection = null;
            _lastMouseTargetPosition = null;
        }
    }
}
