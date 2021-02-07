using System;
using Assets.Scripts.Scenes.Api;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Explore
{
    public sealed class ExploreSceneLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly IExploreSetup _exploreSetup;

        public ExploreSceneLoadHook(IExploreSetup exploreSetup)
        {
            _exploreSetup = exploreSetup;
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
            if (!scene.name.Equals("Explore", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _exploreSetup.Setup();
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            TrySwitchScene(scene);
        }
    }
}