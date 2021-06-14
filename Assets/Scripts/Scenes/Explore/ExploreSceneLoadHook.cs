using System;

using Macerus.Game.Api.Scenes;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSceneLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<IExploreSetup> _lazyExploreSetup;
        private readonly Lazy<ISceneManager> _lazySceneManager;

        public ExploreSceneLoadHook(
            Lazy<IExploreSetup> lazyExploreSetup,
            Lazy<ISceneManager> lazySceneManager)
        {
            _lazyExploreSetup = lazyExploreSetup;
            _lazySceneManager = lazySceneManager;

            _lazySceneManager.Value.SceneChanged += SceneManager_SceneChanged;
        }

        public void Dispose()
        {
            _lazySceneManager.Value.SceneChanged -= SceneManager_SceneChanged;
        }

        private void TrySwitchScene(string sceneName)
        {
            if (!sceneName.Equals("Explore", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _lazyExploreSetup.Value.Setup();
        }

        private void SceneManager_SceneChanged(object sender, EventArgs e)
        {
            TrySwitchScene(_lazySceneManager.Value.CurrentSceneName);
        }
    }
}