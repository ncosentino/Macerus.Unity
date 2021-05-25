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

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Editor
{
    public sealed class MapEditor : IMapEditor
    {
        private Lazy<ISceneToMapConverter> _lazySceneToMapConverter;
        private Lazy<ISerializer> _lazySerializer;
        private Lazy<IDeserializer> _lazyDeserializer;
        private Lazy<IMapFormatter> _lazyMapFormatter;

        public MapEditor(
            Lazy<ISceneToMapConverter> lazySceneToMapConverter,
            Lazy<ISerializer> lazySerializer,
            Lazy<IDeserializer> lazyDeserializer,
            Lazy<IMapFormatter> lazyMapFormatter)
        {
            _lazySceneToMapConverter = lazySceneToMapConverter;
            _lazySerializer = lazySerializer;
            _lazyDeserializer = lazyDeserializer;
            _lazyMapFormatter = lazyMapFormatter;
        }

        private ISceneToMapConverter SceneToMapConverter => _lazySceneToMapConverter.Value;
        private ISerializer Serializer => _lazySerializer.Value;
        private IDeserializer Deserializer => _lazyDeserializer.Value;
        private IMapFormatter MapFormatter => _lazyMapFormatter.Value;

        public void ClearCurrentMap() => ClearCurrentMap(GetMapPrefab());

        public void SaveMap(
            string mapPathToSave,
            Func<string, Stream> openNewWriteableStreamCallback)
        {
            if (mapPathToSave.EndsWith(".objects.json", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = mapPathToSave.Replace(".objects.json", ".json");
                Debug.LogWarning(
                    $"Selected map object file '{mapPathToSave}' so attempting to save '{trimmed}'...");
                mapPathToSave = trimmed;
            }

            Debug.Log($"Converting Unity->Macerus...");
            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            var gameObjects = SceneToMapConverter
                .ConvertGameObjects(mapPrefab)
                .ToArray();
            var mapTiles = SceneToMapConverter
                .ConvertTiles(mapPrefab)
                .ToArray();
            Debug.Log($"Converted Unity->Macerus.");

            var mapGameObjectsPath = mapPathToSave.Replace(".json", ".objects.json");
            Debug.Log($"Writing map game objects to '{mapGameObjectsPath}'...");
            using (var outputStream = openNewWriteableStreamCallback.Invoke(mapGameObjectsPath))
            {
                Serializer.Serialize(outputStream, gameObjects, Encoding.UTF8);
            }

            Debug.Log($"Wrote map game objects to '{mapGameObjectsPath}'.");

            Debug.LogError($"// FIXME: save the MAP out! (its more than just the tiles)");
        }

        public void LoadMap(
            string mapPathToLoad,
            Func<string, Stream> openNewReadableStreamCallback)
        {
            if (mapPathToLoad.EndsWith(".objects.json", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = mapPathToLoad.Replace(".objects.json", ".json");
                Debug.LogWarning(
                    $"Selected map object file '{mapPathToLoad}' so attempting to load '{trimmed}'...");
                mapPathToLoad = trimmed;
            }

            Debug.Log($"Loading map '{mapPathToLoad}'...");
            IGameObject map;
            using (var inputStream = openNewReadableStreamCallback.Invoke(mapPathToLoad))
            {
                map = Deserializer.Deserialize<IGameObject>(inputStream);
            }

            Debug.Log($"Loaded map '{mapPathToLoad}'.");

            var mapObjectsPathToLoad = mapPathToLoad.Replace(".json", ".objects.json");
            Debug.Log($"Loading map objects from '{mapObjectsPathToLoad}'...");
            IReadOnlyCollection<IGameObject> mapObjects;
            using (var inputStream = openNewReadableStreamCallback.Invoke(mapObjectsPathToLoad))
            {
                mapObjects = Deserializer.Deserialize<IReadOnlyCollection<IGameObject>>(inputStream);
            }

            Debug.Log($"Loaded map objects from '{mapObjectsPathToLoad}'.");

            var mapPrefab = GetMapPrefab();
            ClearCurrentMap(mapPrefab);

            Debug.Log("Populating map...");
            MapFormatter.FormatMap(mapPrefab, map);
            MapFormatter.RemoveGameObjects(mapPrefab);
            SceneToMapConverter.ConvertGameObjects(mapPrefab, mapObjects);
            Debug.Log("Populated map.");
        }

        private IMapPrefab GetMapPrefab()
        {
            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            return mapPrefab;
        }

        private void ClearCurrentMap(IMapPrefab mapPrefab)
        {
            Debug.Log("Resetting current map...");
            mapPrefab.Tilemap.ClearAllTiles();
            mapPrefab.Tilemap.ResizeBounds();

            // NOTE: we NEED to keep the ToArray() or otherwise realize the
            // full collection before enumerating so deleting works as expected!
            foreach (var child in mapPrefab.GameObjectLayer.GetChildGameObjects().ToArray())
            {
                UnityEngine.Object.DestroyImmediate(child);
            }

            Debug.Log("Reset current map.");
        }
    }
}
