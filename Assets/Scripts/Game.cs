using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using ProjectXyz.Game.Core.Autofac;
using ProjectXyz.Game.Interface.Engine;
using UnityEngine;

public sealed class Game : MonoBehaviour
{   
    private void Start()
    {
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
            .Discover(moduleDirectory, "*Macerus*.dll")
            .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "*.Dependencies.Autofac.dll"))
            .Concat(moduleDiscoverer
                .Discover(moduleDirectory, "Examples.Modules.*.dll"))
            .ToArray();
        Debug.Log($"Modules loaded.");

        var dependencyContainerBuilder = new DependencyContainerBuilder();
        var dependencyContainer = dependencyContainerBuilder.Create(modules);

        var gameEngine = dependencyContainer.Resolve<IGameEngine>();
    }

    private void Update()
    {

    }
}
