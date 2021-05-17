using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using ProjectXyz.Game.Core.Autofac;

#if UNITY_EDITOR
using UnityEditor.Compilation;
#endif

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Assets.Scripts.Autofac
{
    public sealed class MacerusContainerBuilder
    {
        public IContainer CreateContainer()
        {
            LogDebug($"Creating dependency container...");

#if BLEND
            var moduleDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
#elif UNITY_EDITOR
            var moduleDirectory = Path.Combine(Application.dataPath, "MacerusPlugins");
#else
            var moduleDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin_Data\\Managed");
#endif

            var moduleDiscoverer = new ModuleDiscoverer();
            moduleDiscoverer.AssemblyLoadFailed += (_, e) =>
            {
                LogWarning(
                    $"Failed to load '{e.AssemblyFilePath}'.\r\n" +
                    $"{e.Exception.GetType().FullName}\r\n" +
                    $"{e.Exception.Message}");
                e.Handled = true;
            };
            moduleDiscoverer.AssemblyLoaded += (_, e) =>
            {
                LogDebug($"Loaded '{e.Assembly.FullName}'.");
            };

            LogDebug($"Loading modules from '{moduleDirectory}'...");

            var assemblyModules =
#if UNITY_EDITOR
                CompilationPipeline
                    .GetAssemblies(AssembliesType.Editor)
                    .Where(unityAsm => 
                        unityAsm.name.StartsWith("Assembly-CSharp", StringComparison.OrdinalIgnoreCase) ||
                        unityAsm.name.StartsWith("Main", StringComparison.OrdinalIgnoreCase) ||
                        unityAsm.name.StartsWith("EditorModeTests", StringComparison.OrdinalIgnoreCase) ||
                        unityAsm.name.StartsWith("ContentCreator", StringComparison.OrdinalIgnoreCase))
                    .Select(unityAsm => System.Reflection.Assembly.LoadFile(Path.Combine(
                        Environment.CurrentDirectory,
                        unityAsm.outputPath)))
                    .SelectMany(asm => moduleDiscoverer
                        .Discover(asm));
#else
                moduleDiscoverer.Discover(System.Reflection.Assembly.GetExecutingAssembly());
#endif

            var modules = assemblyModules
                .Concat(moduleDiscoverer
                    .Discover(moduleDirectory, "*.dll"))
                .ToArray();
            foreach (var module in modules)
            {
                LogDebug($"\tLoading module '{module}'.");
            }

            LogDebug($"Modules loaded.");

            var dependencyContainerBuilder = new DependencyContainerBuilder();
            var dependencyContainer = dependencyContainerBuilder.Create(modules);

            LogDebug($"Dependency container created.");
            return dependencyContainer;
        }

        private void LogDebug(string message)
        {
#if UNITY_5_3_OR_NEWER
            Debug.Log(message);
#else
            Console.WriteLine(message);
#endif
        }

        private void LogWarning(string message)
        {
#if UNITY_5_3_OR_NEWER
            Debug.LogWarning(message);
#else
            Console.WriteLine("WARNING" + message);
#endif
        }
    }
}
