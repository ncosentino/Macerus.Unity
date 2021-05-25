using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Assets.Scripts.Plugins.Features.Maps;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Mapping.Default;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Editor
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class MapEditor : IMapEditor
    {
        private readonly Lazy<ISceneToMapConverter> _lazySceneToMapConverter;
        private readonly Lazy<ISerializer> _lazySerializer;
        private readonly Lazy<IDeserializer> _lazyDeserializer;
        private readonly Lazy<IMapFormatter> _lazyMapFormatter;
        private readonly Lazy<IGameObjectFactory> _lazyGameObjectFactory;
        private readonly Lazy<ILogger> _lazyLogger;

        public MapEditor(
            Lazy<ISceneToMapConverter> lazySceneToMapConverter,
            Lazy<ISerializer> lazySerializer,
            Lazy<IDeserializer> lazyDeserializer,
            Lazy<IMapFormatter> lazyMapFormatter,
            Lazy<IGameObjectFactory> lazyGameObjectFactory,
            Lazy<ILogger> lazyLogger)
        {
            _lazySceneToMapConverter = lazySceneToMapConverter;
            _lazySerializer = lazySerializer;
            _lazyDeserializer = lazyDeserializer;
            _lazyMapFormatter = lazyMapFormatter;
            _lazyGameObjectFactory = lazyGameObjectFactory;
            _lazyLogger = lazyLogger;
        }

        private ISceneToMapConverter SceneToMapConverter => _lazySceneToMapConverter.Value;
        private ISerializer Serializer => _lazySerializer.Value;
        private IDeserializer Deserializer => _lazyDeserializer.Value;
        private IMapFormatter MapFormatter => _lazyMapFormatter.Value;
        private IGameObjectFactory GameObjectFactory => _lazyGameObjectFactory.Value;
        private ILogger Logger => _lazyLogger.Value;

        public void ClearCurrentMap() => ClearCurrentMap(GetMapPrefab());

        public void SaveMap(
            string mapPathToSave,
            Func<string, Stream> openNewWriteableStreamCallback)
        {
            if (mapPathToSave.EndsWith(".objects.json", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = mapPathToSave.Replace(".objects.json", ".json");
                Logger.Warn(
                    $"Selected map object file '{mapPathToSave}' so attempting to save '{trimmed}'...");
                mapPathToSave = trimmed;
            }

            Logger.Debug($"Converting Unity->Macerus...");
            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            var gameObjects = SceneToMapConverter
                .ConvertGameObjects(mapPrefab)
                .ToArray();
            var mapTiles = SceneToMapConverter
                .ConvertTiles(mapPrefab)
                .ToArray();
            var map = GameObjectFactory.Create(new IBehavior[]
            {
                // FIXME: also write out the map properties behaviors!!!
                new MapLayersBehavior(new[]
                {
                    new MapLayer("Tiles", mapTiles),
                }),
            });
            Logger.Debug($"Converted Unity->Macerus.");

            Logger.Debug($"Writing map to '{mapPathToSave}'...");
            using (var outputStream = openNewWriteableStreamCallback.Invoke(mapPathToSave))
            {
                Serializer.Serialize(outputStream, map, Encoding.UTF8);
            }

            Logger.Debug($"Wrote map to '{mapPathToSave}'.");

            var mapGameObjectsPath = mapPathToSave.Replace(".json", ".objects.json");
            Logger.Debug($"Writing map game objects to '{mapGameObjectsPath}'...");
            using (var outputStream = openNewWriteableStreamCallback.Invoke(mapGameObjectsPath))
            {
                Serializer.Serialize(outputStream, gameObjects, Encoding.UTF8);
            }

            Logger.Debug($"Wrote map game objects to '{mapGameObjectsPath}'.");
        }

        public void LoadMap(
            string mapPathToLoad,
            Func<string, Stream> openNewReadableStreamCallback)
        {
            if (mapPathToLoad.EndsWith(".objects.json", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = mapPathToLoad.Replace(".objects.json", ".json");
                Logger.Warn(
                    $"Selected map object file '{mapPathToLoad}' so attempting to load '{trimmed}'...");
                mapPathToLoad = trimmed;
            }

            Logger.Debug($"Loading map '{mapPathToLoad}'...");
            IGameObject map;
            using (var inputStream = openNewReadableStreamCallback.Invoke(mapPathToLoad))
            {
                map = Deserializer.Deserialize<IGameObject>(inputStream);
            }

            Logger.Debug($"Loaded map '{mapPathToLoad}'.");

            var mapObjectsPathToLoad = mapPathToLoad.Replace(".json", ".objects.json");
            Logger.Debug($"Loading map objects from '{mapObjectsPathToLoad}'...");
            IReadOnlyCollection<IGameObject> mapObjects;
            using (var inputStream = openNewReadableStreamCallback.Invoke(mapObjectsPathToLoad))
            {
                mapObjects = Deserializer.Deserialize<IReadOnlyCollection<IGameObject>>(inputStream);
            }

            Logger.Debug($"Loaded map objects from '{mapObjectsPathToLoad}'.");

            var mapPrefab = GetMapPrefab();
            ClearCurrentMap(mapPrefab);

            Logger.Debug("Populating map...");
            // FIXME: also put the map properties behaviors somewhere in the editor!
            MapFormatter.FormatMap(mapPrefab, map);
            MapFormatter.RemoveGameObjects(mapPrefab);
            SceneToMapConverter.ConvertGameObjects(mapPrefab, mapObjects);
            Logger.Debug("Populated map.");
        }

        private IMapPrefab GetMapPrefab()
        {
            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            return mapPrefab;
        }

        private void ClearCurrentMap(IMapPrefab mapPrefab)
        {
            Logger.Debug("Resetting current map...");
            mapPrefab.Tilemap.ClearAllTiles();
            mapPrefab.Tilemap.ResizeBounds();

            // NOTE: we NEED to keep the ToArray() or otherwise realize the
            // full collection before enumerating so deleting works as expected!
            foreach (var child in mapPrefab.GameObjectLayer.GetChildGameObjects().ToArray())
            {
                UnityEngine.Object.DestroyImmediate(child);
            }

            Logger.Debug("Reset current map.");
        }
    }
}
