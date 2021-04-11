using System.Linq;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

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

        private GameObject GetSingleInstanceOfConsole()
        {
            if (_debugConsole != null)
            {
                return _debugConsole;
            }

            var allDebugConsoleObjects = _gameObjectManager
                .FindAll(x => x.name == "DebugConsole")
                .ToList();
            Contract.Requires(
                allDebugConsoleObjects.Count == 1,
                $"Expecting to find only one DebugConsole but there were " +
                $"{allDebugConsoleObjects.Count}. Perhaps check the singleton " +
                $"behavior of this component.");

            var singleInstance = allDebugConsoleObjects.Single();
            _debugConsole = singleInstance;
            return singleInstance;
        }
    }
}