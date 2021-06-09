using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using Macerus.Game.Api;
using Macerus.Plugins.Features.CharacterSheet.Api;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Api.Crafting;

using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IPlayerInventoryController _playerInventoryController;
        private readonly ICharacterSheetController _characterSheetController;
        private readonly ICraftingController _craftingController;
        private readonly ISceneManager _sceneManager;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            IKeyboardControls keyboardControls,
            ISceneManager sceneManager,
            IPlayerInventoryController playerInventoryController,
            ICharacterSheetController characterSheetController,
            ICraftingController craftingController)
        {
            _debugConsoleManager = debugConsoleManager;
            _keyboardControls = keyboardControls;
            _sceneManager = sceneManager;
            _playerInventoryController = playerInventoryController;
            _characterSheetController = characterSheetController;
            _craftingController = craftingController;
        }

        public void Update(float deltaTime)
        {
            if (_debugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (UnityEngine.Input.GetKeyUp(_keyboardControls.GuiClose))
            {
                _sceneManager.GoToScene(new StringIdentifier("MainMenu"));
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleInventory))
            {
                _playerInventoryController.ToggleInventory();
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleCrafting))
            {
                _craftingController.ToggleCraftingWindow();
            }
            else if (UnityEngine.Input.GetKeyUp(_keyboardControls.ToggleCharacterSheet))
            {
                _characterSheetController.ToggleCharacterSheet();
            }
        }
    }
}
