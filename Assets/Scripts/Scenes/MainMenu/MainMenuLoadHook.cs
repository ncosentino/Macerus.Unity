using System;

using Assets.Scripts.Scenes.Api;

using Macerus.Plugins.Features.Gui.Api.SceneTransitions;

using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<IMainMenuSetup> _lazyMainMenuSetup;
        private readonly Lazy<ISceneTransitionController> _sceneTransitionController;

        private bool _lastSceneWasMainMenu;

        public MainMenuLoadHook(
            Lazy<IMainMenuSetup> mainMenuSetup,
            Lazy<ISceneTransitionController> sceneTransitionController)
        {
            _lazyMainMenuSetup = mainMenuSetup;
            _sceneTransitionController = sceneTransitionController;

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        public void SwitchScene()
        {
            TrySwitchScene(SceneManager.GetActiveScene());
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        private void TrySwitchScene(Scene scene)
        {
            if (!scene.name.Equals("MainMenu", StringComparison.OrdinalIgnoreCase))
            {
                _lastSceneWasMainMenu = false;
                return;
            }

            _lazyMainMenuSetup.Value.Setup();

            if (!_lastSceneWasMainMenu)
            {
                _sceneTransitionController.Value.StartTransition(
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(3),
                    null,
                    null);
            }

            _lastSceneWasMainMenu = true;
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            TrySwitchScene(scene);
        }
    }
}