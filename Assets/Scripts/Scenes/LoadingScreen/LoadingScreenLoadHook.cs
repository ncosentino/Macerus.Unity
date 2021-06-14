using System;

using Assets.Scripts.Scenes.MainMenu;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.Api.SceneTransitions;

namespace Assets.Scripts.Scenes.LoadingScreen
{
    public sealed class LoadingScreenLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<ILoadingScreenSetup> _lazyLoadingScreenSetup;
        private readonly Lazy<ISceneTransitionController> _sceneTransitionController;
        private readonly Lazy<ISceneManager> _lazySceneManager;

        public LoadingScreenLoadHook(
            Lazy<ILoadingScreenSetup> loadingScreenSetup,
            Lazy<ISceneTransitionController> sceneTransitionController,
            Lazy<ISceneManager> lazySceneManager)
        {
            _lazyLoadingScreenSetup = loadingScreenSetup;
            _sceneTransitionController = sceneTransitionController;
            _lazySceneManager = lazySceneManager;

            lazySceneManager.Value.SceneChanged += SceneManager_SceneChanged;
        }

        public void Dispose()
        {
            _lazySceneManager.Value.SceneChanged -= SceneManager_SceneChanged;
        }

        private void TrySwitchScene(string sceneName)
        {
            if (!sceneName.Equals("LoadingScreen", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _lazyLoadingScreenSetup.Value.Setup();
        }

        private void SceneManager_SceneChanged(object sender, EventArgs e)
        {
            TrySwitchScene(_lazySceneManager.Value.CurrentSceneName);
        }
    }
}