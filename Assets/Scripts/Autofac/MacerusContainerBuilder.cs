using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

            var moduleDirectory = Path.Combine(Application.dataPath, "Dependencies");
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
            Debug.Log($"Modules loaded.");

            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            Debug.Log($"Dependency container created.");
            return dependencyContainer;
        }
    }
}
