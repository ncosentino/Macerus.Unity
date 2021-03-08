using System;

using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu
{
    public sealed class MainMenuController
    {
        private IReadOnlyMainMenuViewModel _mainMenuViewModel;

        public MainMenuController(IReadOnlyMainMenuViewModel mainMenuViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _mainMenuViewModel.ExitRequested += MainMenuViewModel_ExitRequested;
            _mainMenuViewModel.NewGameRequested += MainMenuViewModel_NewGameRequested;
        }

        private void MainMenuViewModel_NewGameRequested(object sender, EventArgs e)
        {
            SceneManager.LoadScene("Explore");
        }

        private void MainMenuViewModel_ExitRequested(object sender, EventArgs e)
        {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            global::UnityEditor.EditorApplication.isPlaying = false;
#else
         UnityEngine.Application.Quit();
#endif
        }
    }
}