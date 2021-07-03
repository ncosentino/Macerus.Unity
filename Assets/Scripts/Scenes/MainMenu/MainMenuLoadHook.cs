using System;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.SceneTransitions;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<IMainMenuSetup> _lazyMainMenuSetup;
        private readonly Lazy<ITransitionController> _lazyTransitionController;
        private readonly Lazy<ISceneManager> _lazySceneManager;

        private bool _lastSceneWasMainMenu;

        public MainMenuLoadHook(
            Lazy<IMainMenuSetup> mainMenuSetup,
            Lazy<ITransitionController> lazyTransitionController,
            Lazy<ISceneManager> lazySceneManager)
        {
            _lazyMainMenuSetup = mainMenuSetup;
            _lazyTransitionController = lazyTransitionController;
            _lazySceneManager = lazySceneManager;
            
            lazySceneManager.Value.SceneChanged += SceneManager_SceneChanged;
        }

        public void Dispose()
        {
            _lazySceneManager.Value.SceneChanged -= SceneManager_SceneChanged;
        }

        private void TrySwitchScene(string sceneName)
        {
            if (!sceneName.Equals("MainMenu", StringComparison.OrdinalIgnoreCase))
            {
                _lastSceneWasMainMenu = false;
                return;
            }

            _lazyMainMenuSetup.Value.Setup();

            if (!_lastSceneWasMainMenu)
            {
                _lazyTransitionController.Value.StartTransition(
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(3),
                    null,
                    null);
            }

            _lastSceneWasMainMenu = true;
        }

        private void SceneManager_SceneChanged(object sender, EventArgs e)
        {
            TrySwitchScene(_lazySceneManager.Value.CurrentSceneName);
        }
    }
}