using System;
using System.Collections;

using Assets.Scripts.Unity.Threading;

using Macerus.Game.Api.Scenes;

using ProjectXyz.Api.Framework;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Application
{
    using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

    public sealed class SceneManager : ISceneManager
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneManager(ICoroutineRunner coroutineRunner)
        {
            UnitySceneManager.sceneLoaded += UnitySceneManager_sceneLoaded;
            _coroutineRunner = coroutineRunner;
        }

        public event EventHandler<EventArgs> SceneChanged;

        public string CurrentSceneName => UnitySceneManager.GetActiveScene().name;

        public void NavigateToScene(IIdentifier sceneId)
        {
            UnitySceneManager.LoadScene(sceneId.ToString());
        }

        public void BeginNavigateToScene(IIdentifier sceneId, Action<ISceneCompletion> completedCallback)
        {
            _coroutineRunner.StartCoroutine(
                LoadScene(sceneId, completedCallback),
                errorCallback: null);
        }

        private IEnumerator LoadScene(
            IIdentifier sceneId,
            Action<ISceneCompletion> completedCallback)
        {
            yield return null;

            // Begin to load the Scene you specify
            AsyncOperation asyncOperation = UnitySceneManager.LoadSceneAsync(sceneId.ToString());
            
            // Don't let the Scene activate until you allow it to
            asyncOperation.allowSceneActivation = false;

            // When the load is still in progress, output the Text and progress bar
            while (!asyncOperation.isDone)
            {
                // Check if the load has finished
                if (asyncOperation.progress >= 0.9f)
                {
                    var sceneCompletion = new SceneCompletion(asyncOperation);
                    if (completedCallback == null)
                    {
                        sceneCompletion.SwitchoverScenes();
                    }
                    else
                    {
                        completedCallback.Invoke(sceneCompletion);
                    }
                }

                yield return null;
            }
        }

        private void UnitySceneManager_sceneLoaded(
            UnityEngine.SceneManagement.Scene scene,
            UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
        {
            SceneChanged?.Invoke(this, EventArgs.Empty);
        }

        private sealed class SceneCompletion : ISceneCompletion
        {
            private readonly AsyncOperation _asyncOperation;

            public SceneCompletion(AsyncOperation asyncOperation)
            {
                _asyncOperation = asyncOperation;
            }

            public void SwitchoverScenes()
            {
                _asyncOperation.allowSceneActivation = true;
            }
        }
    }
}
