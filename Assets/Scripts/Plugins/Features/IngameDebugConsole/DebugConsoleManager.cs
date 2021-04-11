using System.Linq;
using Assets.Scripts.Plugins.Features.IngameDebugConsole.Api;
using Assets.Scripts.Unity.GameObjects;

using IngameDebugConsole;

namespace Assets.Scripts.Plugins.Features.IngameDebugConsole
{
    public sealed class DebugConsoleManager : IDebugConsoleManager
    {
        private readonly IUnityGameObjectManager _gameObjectManager;
        
        private DebugLogManager _debugConsole;

        public DebugConsoleManager(IUnityGameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public bool GetConsoleWindowVisible()
        {
            var consoleWindow = GetSingleInstanceOfConsole();
            return consoleWindow.IsLogWindowVisible;
        }

        private DebugLogManager GetSingleInstanceOfConsole()
        {
            if (_debugConsole != null)
            {
                return _debugConsole;
            }

            var allDebugConsoleObjects = _gameObjectManager
                .FindAll(x => x.name == "DebugConsole" && x.activeSelf)
                .Select(x => x.GetComponent<DebugLogManager>())
                .ToList();
            var singleInstance = allDebugConsoleObjects.First();
            _debugConsole = singleInstance;
            return singleInstance;
        }
    }
}