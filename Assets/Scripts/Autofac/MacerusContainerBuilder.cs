using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using ProjectXyz.Game.Core.Autofac;
using UnityEngine;

namespace Assets.Scripts.Autofac
{
    public sealed class MacerusContainerBuilder
    {
        public IContainer CreateContainer()
        {
            Debug.Log($"Creating dependency container...");
            
#if UNITY_EDITOR
            var moduleDirectory = Path.Combine(Application.dataPath, "MacerusPlugins");
#else
            var moduleDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin_Data\\Managed");
#endif

            var moduleDiscoverer = new ModuleDiscoverer();
            moduleDiscoverer.AssemblyLoadFailed += (_, e) =>
            {
                Debug.LogWarning(
                    $"Failed to load '{e.AssemblyFilePath}'.\r\n" +
                    $"{e.Exception.GetType().FullName}\r\n" +
                    $"{e.Exception.Message}");
                e.Handled = true;
            };
            moduleDiscoverer.AssemblyLoaded += (_, e) =>
            {
                Debug.Log($"Loaded '{e.Assembly.FullName}'.");
            };

            Debug.Log($"Loading modules from '{moduleDirectory}'...");
            var modules = moduleDiscoverer
                .Discover(Assembly.GetExecutingAssembly())
                .Concat(moduleDiscoverer
                    .Discover(moduleDirectory, "*.dll"))
                .ToArray();
            foreach (var module in modules)
            {
                Debug.Log($"\tLoading module '{module}'.");
            }

            Debug.Log($"Modules loaded.");

            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            Debug.Log($"Dependency container created.");
            return dependencyContainer;
        }
    }
}
