using Assets.Scripts.Behaviours;
using Assets.Scripts.Gui;

using Autofac;

using IngameDebugConsole;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

using UnityEngine;

namespace Assets.Scripts.Console
{
    public sealed class GlobalConsoleCommandsBehaviour : MonoBehaviour
    {
        private ProjectXyz.Api.Logging.ILogger _logger;
        private IGameObjectManager _gameObjectManager;
        private IGuiPrefabCreator _guiPrefabCreator;
        private IViewWelderFactory _viewWelderFactory;

        private void Start()
        {
            // NOTE: we're violating the sticher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            _logger = container.Resolve<ProjectXyz.Api.Logging.ILogger>();
            _gameObjectManager = container.Resolve<IGameObjectManager>();
            _guiPrefabCreator = container.Resolve<IGuiPrefabCreator>();
            _viewWelderFactory = container.Resolve<IViewWelderFactory>();

            AddCommand(
                nameof(AddTestGui),
                "Adds the WIP gui.");
        }

        private void AddCommand(string name, string description)
        {
            DebugLogConsole.AddCommandInstance(
                name,
                description,
                name,
                this);
        }

        private void AddTestGui()
        {
            var gui = _guiPrefabCreator.Create();
            _viewWelderFactory
                .Create<ISimpleWelder>(gui, new Noesis.Button() { Content = "Hello world" })
                .Weld();
        }
    }
}
