using Assets.Scripts.Autofac;
using Autofac;
using UnityEngine;

namespace Assets.Scripts.Behaviours
{
    public sealed class GameDependencyBehaviour : 
        MonoBehaviour
    {
        private static GameDependencyBehaviour _instance;

        private IContainer _container;

        public static IContainer Container => _instance._container;
        
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
        }
    }
}
