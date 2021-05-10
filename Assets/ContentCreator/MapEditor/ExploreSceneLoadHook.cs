using System;
using Assets.Scripts.Scenes.Api;
using UnityEngine.SceneManagement;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class ExploreSceneLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<IExploreSetup> _lazyExploreSetup;

        public ExploreSceneLoadHook(Lazy<IExploreSetup> exploreSetup)
        {
            _lazyExploreSetup = exploreSetup;
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
            if (!scene.name.Equals("MapEditor", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _lazyExploreSetup.Value.Setup();
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            TrySwitchScene(scene);
        }
    }
}