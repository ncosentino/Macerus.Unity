using Assets.Scripts.Autofac;

using Autofac;

using Macerus.Game.Api.Scenes;

using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    public sealed class GameDependencyBehaviour : MonoBehaviour
    {
        private static GameDependencyBehaviour _instance;

        private IContainer _container;

        public static IContainer Container => _instance._container;

        public static GameDependencyBehaviour Instance => _instance;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameDependencies").AddComponent<GameDependencyBehaviour>();
                DontDestroyOnLoad(_instance.gameObject);
            }
        }

        private void Start()
        {
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Debug.Log("****DEBUGGER IS NOT ATTACHED****");
            }
#endif

            Debug.Log("Creating autofac container...");
            var containerBuilder = new MacerusContainerBuilder();
            _container = containerBuilder.CreateContainer();
            Debug.Log("Created autofac container.");

            var sceneManager = _container.Resolve<ISceneManager>();
            if (!string.Equals(
                "Startup",
                sceneManager.CurrentSceneName))
            {
                Debug.LogError(
                    $"Expecting to start in 'Startup' scene, but was started " +
                    $"in '{sceneManager.CurrentSceneName}'. You may experience " +
                    $"unintended behavior, but we'll force an explicit " +
                    $"navigation to '{sceneManager.CurrentSceneName}'");
                sceneManager.NavigateToScene(new StringIdentifier(sceneManager.CurrentSceneName));
                return;
            }

            sceneManager.NavigateToScene(new StringIdentifier("MainMenu"));
        }
    }
}
