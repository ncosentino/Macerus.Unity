using System;

using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Gui.SceneTransitions;

namespace Assets.Scripts.Scenes.LoadingScreen
{
    public sealed class LoadingScreenLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<ILoadingScreenSetup> _lazyLoadingScreenSetup;
        private readonly Lazy<ITransitionController> _lazyTransitionController;
        private readonly Lazy<ISceneManager> _lazySceneManager;

        public LoadingScreenLoadHook(
            Lazy<ILoadingScreenSetup> loadingScreenSetup,
            Lazy<ITransitionController> lazyTransitionController,
            Lazy<ISceneManager> lazySceneManager)
        {
            _lazyLoadingScreenSetup = loadingScreenSetup;
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