using System.Linq;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.GameObjects;

namespace Assets.Scripts.Plugins.Features.IngameDebugConsole
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class DebugConsoleManager : IDebugConsoleManager
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly ILogger _logger;

        public DebugConsoleManager(
            IGameObjectManager gameObjectManager,
            IObjectDestroyer objectDestroyer,
            ILogger logger)
        {
            _gameObjectManager = gameObjectManager;
            _objectDestroyer = objectDestroyer;
            _logger = logger;
        }

        public void Toggle()
        {
            _logger.Debug("Toggling debug console...");
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

            var singleInstance = allDebugConsoleObjects.Single();
            singleInstance.SetActive(!singleInstance.activeSelf);
            _logger.Debug("Toggled debug console.");
        }
    }
}