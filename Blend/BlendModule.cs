﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

using Autofac;
using Macerus.Game.Api;
using Macerus.Game.Api.Scenes;
using Macerus.Plugins.Features.Animations.Lpc;
using Macerus.Plugins.Features.Mapping;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Logging;

namespace Assets.Blend
{
    public sealed class BlendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ConsoleLogger>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneLpcAnimationDiscovererSettings>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MappingAssetPaths>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapResourceLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<NoneMapTraversableHighlighter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SceneManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<Application>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c => new LpcAnimationDiscovererSettings(
                    $@"..\..\..\assets\resources",
                    @"graphics\actors\LpcUniversal/"))
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private sealed class NoneMapTraversableHighlighter : IMapTraversableHighlighter
        {
            public void SetTargettedTiles(IReadOnlyDictionary<int, HashSet<Vector2>> traversableTiles)
            {
            }

            public void SetTraversableTiles(IEnumerable<Vector2> traversableTiles)
            {
            }
        }

        private sealed class ConsoleLogger : ILogger
        {
            public void Debug(string message) => Debug(message, null);

            public void Debug(string message, object data)
            {
                System.Diagnostics.Debug.WriteLine($"DEBUG: {message}");
                if (data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"\t{data}");
                }
            }

            public void Error(string message)
            {
                throw new NotImplementedException();
            }

            public void Error(string message, object data)
            {
                throw new NotImplementedException();
            }

            public void Info(string message) => Info(message, null);

            public void Info(string message, object data)
            {
                System.Diagnostics.Debug.WriteLine($"INFO: {message}");
                if (data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"\t{data}");
                }
            }

            public void Warn(string message) => Warn(message, null);

            public void Warn(string message, object data)
            {
                System.Diagnostics.Debug.WriteLine($"WARN: {message}");
                if (data != null)
                {
                    System.Diagnostics.Debug.WriteLine($"\t{data}");
                }
            }
        }

        public sealed class MappingAssetPaths : IMappingAssetPaths
        {
            private readonly Lazy<DirectoryInfo> _lazyResourceRoot;
            private readonly Lazy<DirectoryInfo> _lazyMapsRoot;

            public MappingAssetPaths()
            {
                _lazyResourceRoot =
                   new Lazy<DirectoryInfo>(() =>
                   {
                       return new DirectoryInfo(@"..\..\..\Assets\Resources");
                   });
                _lazyMapsRoot =
                    new Lazy<DirectoryInfo>(() =>
                    {
                        return new DirectoryInfo(Path.Combine(_lazyResourceRoot.Value.FullName, @"Mapping\Maps"));
                    });
            }

            public string MapsRoot => _lazyMapsRoot.Value.FullName;

            public string ResourcesRoot => _lazyResourceRoot.Value.FullName;
        }

        public sealed class MapResourceLoader : IMapResourceLoader
        {
            private readonly IMappingAssetPaths _mappingAssetPaths;

            public MapResourceLoader(IMappingAssetPaths mappingAssetPaths)
            {
                _mappingAssetPaths = mappingAssetPaths;
            }

            public async Task<Stream> LoadStreamAsync(string pathToResource)
            {
                var fullPath = Path.Combine(
                    _mappingAssetPaths.ResourcesRoot,
                    $"{pathToResource}.json");
                return File.OpenRead(fullPath);
            }
        }

        public sealed class Application : IApplication
        {
            public void Exit() => Environment.Exit(0);
        }

        public sealed class SceneManager : ISceneManager
        {
            private readonly ILogger _logger;

            public SceneManager(ILogger logger)
            {
                _logger = logger;
            }

            public string CurrentSceneName => throw new NotImplementedException();

            public event EventHandler<EventArgs> SceneChanged;

            public void BeginNavigateToScene(IIdentifier sceneId, Action<ISceneCompletion> completedCallback)
            {
                _logger.Info($"Begin navigate to scene '{sceneId}'...");
            }

            public void NavigateToScene(IIdentifier sceneId)
            {
                _logger.Info($"Navigate to scene '{sceneId}'.");
            }
        }
    }    
}
