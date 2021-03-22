using System.Linq;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.IngameDebugConsole
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class DebugConsoleManager : IDebugConsoleManager
    {
        private readonly IUnityGameObjectManager _gameObjectManager;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly ILogger _logger;
        
        private GameObject _debugConsole;

        public DebugConsoleManager(
            IUnityGameObjectManager gameObjectManager,
            IObjectDestroyer objectDestroyer,
            ILogger logger)
        {
            _gameObjectManager = gameObjectManager;
            _objectDestroyer = objectDestroyer;
            _logger = logger;
        }

        public bool GetConsoleWindowVisible()
        {
            var consoleWindow = GetSingleInstanceOfConsole();
            if (!consoleWindow.activeSelf)
            {
                return false;
            }

            return consoleWindow
                .GetChildGameObjects()
                .First(x => x.name == "DebugLogWindow")
                .GetComponent<CanvasGroup>()
                .interactable;
        }

        public void Toggle()
        {
            _logger.Debug("Toggling debug console...");
            var singleInstance = GetSingleInstanceOfConsole();
            singleInstance.SetActive(!singleInstance.activeSelf);
            _logger.Debug("Toggled debug console.");
        }

        private GameObject GetSingleInstanceOfConsole()
        {
            if (_debugConsole != null)
            {
                return _debugConsole;
            }

            var allDebugConsoleObjects = _gameObjectManager
                .FindAll(x => x.name == "DebugConsole")
                .ToList();
            var enabledInstances = allDebugConsoleObjects
                .Where(x => x.activeSelf)
                .ToArray();
            var disabledInstances = allDebugConsoleObjects
                .Where(x => !x.activeSelf)
                .ToArray();

            if (enabledInstances.Any())
            {
                foreach (var gameObject in enabledInstances.Skip(1))
                {
                    allDebugConsoleObjects.Remove(gameObject);
                    _objectDestroyer.Destroy(gameObject);
                }

                foreach (var gameObject in disabledInstances)
                {
                    allDebugConsoleObjects.Remove(gameObject);
                    _objectDestroyer.Destroy(gameObject);
                }
            }
            else
            {
                foreach (var gameObject in disabledInstances.Skip(1))
                {
                    allDebugConsoleObjects.Remove(gameObject);
                    _objectDestroyer.Destroy(gameObject);
                }
            }

            var singleInstance = allDebugConsoleObjects.Single();
            _debugConsole = singleInstance;
            return singleInstance;
        }
    }
}