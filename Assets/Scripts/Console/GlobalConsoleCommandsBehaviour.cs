using Assets.Scripts.Behaviours;

using Autofac;

using IngameDebugConsole;

using UnityEngine;

namespace Assets.Scripts.Console
{
    public sealed class GlobalConsoleCommandsBehaviour : MonoBehaviour
    {
        private ProjectXyz.Api.Logging.ILogger _logger;

        private void Start()
        {
            // NOTE: we're violating the sticher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            _logger = container.Resolve<ProjectXyz.Api.Logging.ILogger>();
        }

        private void AddCommand(string name, string description)
        {
            DebugLogConsole.AddCommandInstance(
                name,
                description,
                name,
                this);
        }
    }
}
