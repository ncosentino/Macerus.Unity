using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.Input;

using Macerus.Plugins.Features.StatusBar.Api;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public sealed class PlayerQuickSlotControlsBehaviour : MonoBehaviour
    {
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

        private void Start()
        {
            UnityContracts.RequiresNotNull(this, Logger, nameof(Logger));
            UnityContracts.RequiresNotNull(this, KeyboardInput, nameof(KeyboardInput));
            UnityContracts.RequiresNotNull(this, KeyboardControls, nameof(KeyboardControls));
            UnityContracts.RequiresNotNull(this, DebugConsoleManager, nameof(DebugConsoleManager));
            UnityContracts.RequiresNotNull(this, StatusBarController, nameof(StatusBarController));

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
            await HandleQuickSlotControlsAsync();
        }

        private async Task HandleQuickSlotControlsAsync()
        {
            if (DebugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            foreach (var entry in _keyToSlotIndex.OrderBy(x => x.Value))
            {
                if (KeyboardInput.GetKeyUp(entry.Key))
                {
                    await StatusBarController.ActivateSkillSlotAsync(entry.Value);
                    break;
                }
            }
        }
    }


}
