using System.Linq;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Unity.IngameDebugConsole
{
    public sealed class DebugConsoleManager : IDebugConsoleManager
    {
        private readonly IGameObjectManager _gameObjectManager;
        private readonly IObjectDestroyer _objectDestroyer;

        public DebugConsoleManager(
            IGameObjectManager gameObjectManager,
            IObjectDestroyer objectDestroyer)
        {
            _gameObjectManager = gameObjectManager;
            _objectDestroyer = objectDestroyer;
        }

        public void Toggle()
        {
            Debug.Log("Toggling debug console...");
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
            Debug.Log("Toggled debug console.");
        }
    }
}