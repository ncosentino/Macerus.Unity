using System;

using Assets.Scripts.Scenes.Api;

using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuLoadHook : IDiscoverableSceneLoadHook
    {
        private readonly Lazy<IMainMenuSetup> _lazyMainMenuSetup;

        public MainMenuLoadHook(Lazy<IMainMenuSetup> mainMenuSetup)
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            _lazyMainMenuSetup = mainMenuSetup;
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
                return;
            }

            _lazyMainMenuSetup.Value.Setup();
        }

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode sceneLoadMode)
        {
            TrySwitchScene(scene);
        }
    }
}