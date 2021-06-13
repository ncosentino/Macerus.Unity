using Assets.Scripts.Input.Api;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;

using Macerus.Plugins.Features.CharacterSheet.Api;
using Macerus.Plugins.Features.Hud;
using Macerus.Plugins.Features.InGameMenu.Api;
using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Inventory.Api.Crafting;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public sealed class GuiInputController : IGuiInputController
    {
        private readonly IDebugConsoleManager _debugConsoleManager;
        private readonly IKeyboardControls _keyboardControls;
        private readonly IPlayerInventoryController _playerInventoryController;
        private readonly ICharacterSheetController _characterSheetController;
        private readonly ICraftingController _craftingController;
        private readonly IHudViewModel _hudViewModel;
        private readonly IHudController _hudController;
        private readonly IInGameMenuViewModel _inGameMenuViewModel;
        private readonly IInGameMenuController _inGameMenuController;

        public GuiInputController(
            IDebugConsoleManager debugConsoleManager,
            IKeyboardControls keyboardControls,
            IPlayerInventoryController playerInventoryController,
            ICharacterSheetController characterSheetController,
            ICraftingController craftingController,
            IHudViewModel hudViewModel,
            IHudController hudController,
            IInGameMenuViewModel inGameMenuViewModel,
            IInGameMenuController inGameMenuController)
        {
            _debugConsoleManager = debugConsoleManager;
            _keyboardControls = keyboardControls;
            _playerInventoryController = playerInventoryController;
            _characterSheetController = characterSheetController;
            _craftingController = craftingController;
            _hudViewModel = hudViewModel;
            _hudController = hudController;
            _inGameMenuViewModel = inGameMenuViewModel;
            _inGameMenuController = inGameMenuController;
        }

        public void Update(float deltaTime)
        {
            if (_debugConsoleManager.GetConsoleWindowVisible())
            {
                return;
            }

            if (UnityEngine.Input.GetKeyUp(_keyboardControls.GuiClose))
            {
                if (_hudViewModel.AnyWindowsOpen())
                {
                    _hudController.CloseAllWindows();
                }
                else if (_inGameMenuViewModel.IsOpen)
                {
                    _inGameMenuController.CloseMenu();
                }
                else
                {
                    _inGameMenuController.OpenMenu();
                }
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
