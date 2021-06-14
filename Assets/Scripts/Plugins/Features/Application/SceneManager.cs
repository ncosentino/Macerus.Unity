using System;

using Macerus.Game.Api.Scenes;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Application
{
    using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

    public sealed class SceneManager : ISceneManager
    {
        public SceneManager()
        {
            UnitySceneManager.sceneLoaded += UnitySceneManager_sceneLoaded;
        }

        public event EventHandler<EventArgs> SceneChanged;

        public string CurrentSceneName => UnitySceneManager.GetActiveScene().name;

        public void GoToScene(IIdentifier sceneId)
        {
            UnitySceneManager.LoadScene(sceneId.ToString());
        }

        private void UnitySceneManager_sceneLoaded(
            UnityEngine.SceneManagement.Scene scene,
            UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
        {
            SceneChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
