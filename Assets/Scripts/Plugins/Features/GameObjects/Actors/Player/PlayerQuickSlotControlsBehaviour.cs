using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;
using Assets.Scripts.Unity.Threading;

using Macerus.Plugins.Features.StatusBar.Api;

using NexusLabs.Contracts;

using ProjectXyz.Api.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerQuickSlotControlsBehaviour : MonoBehaviour
    {
        private readonly UnityAsynRunner _runner = new UnityAsynRunner();
        private readonly Dictionary<KeyCode, int> _keyToSlotIndex;
        
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

        private void Start()
        {
            this.RequiresNotNull(Logger, nameof(Logger));
            this.RequiresNotNull(KeyboardInput, nameof(KeyboardInput));
            this.RequiresNotNull(KeyboardControls, nameof(KeyboardControls));
            this.RequiresNotNull(DebugConsoleManager, nameof(DebugConsoleManager));
            this.RequiresNotNull(StatusBarController, nameof(StatusBarController));
            this.RequiresNotNull(ActorActionCheck, nameof(ActorActionCheck));
            this.RequiresNotNull(Actor, nameof(Actor));

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
        }

        private async Task Update()
        {
            await _runner.RunAsync(HandleQuickSlotControlsAsync);
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

            foreach (var entry in _keyToSlotIndex.OrderBy(x => x.Value))
            {
                if (KeyboardInput.GetKeyUp(entry.Key))
                {
                    await StatusBarController
                        .ActivateSkillSlotAsync(
                            Actor,
                            entry.Value)
                        .ConfigureAwait(false);
                    break;
                }

                if (KeyboardInput.GetKeyDown(entry.Key))
                {
                    await StatusBarController
                        .PreviewSkillSlotAsync(
                            Actor,
                            entry.Value)
                        .ConfigureAwait(false);
                    break;
                }
            }
        }
    }
}
