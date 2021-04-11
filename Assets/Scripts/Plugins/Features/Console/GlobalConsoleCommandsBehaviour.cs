using Assets.Scripts.Behaviours;

using Autofac;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Console
{
    public sealed class GlobalConsoleCommandsBehaviour : MonoBehaviour
    {
        private void Start()
        {
            // NOTE: we're violating the stitcher pattern here and *YES* using 
            // the most evil service locator pattern here BUT here me out...
            // This class will probably be mostly for test methods and this is 
            // the easiest shortcut for getting access to all kinds of stuff
            // without making separate classes for stitching etc...
            var container = GameDependencyBehaviour.Container;

            container
                .Resolve<IConsoleCommandRegistrar>()
                .RegisterDiscoverableCommandsFromInstance(this);
        }

        // FIXME: define commands here with the discoverable attribute
    }
}
