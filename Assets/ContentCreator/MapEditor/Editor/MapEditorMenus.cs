using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Assets.Scripts.Autofac;
using Assets.Scripts.Plugins.Features.Maps;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;

using Autofac;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.ContentCreator.MapEditor.Editor
{
    public sealed class MapEditorMenus
    {
        private static Lazy<IContainer> _lazyContainer = new
            Lazy<IContainer>(() => new MacerusContainerBuilder().CreateContainer());
        private static Lazy<ISceneToMapConverter> _lazySceneToMapConverter =
            new Lazy<ISceneToMapConverter>(() => Container.Resolve<ISceneToMapConverter>());
        private static Lazy<ISerializer> _lazySerializer =
            new Lazy<ISerializer>(() => Container.Resolve<ISerializer>());
        private static Lazy<IDeserializer> _lazyDeserializer =
            new Lazy<IDeserializer>(() => Container.Resolve<IDeserializer>());
        private static Lazy<IMapFormatter> _lazyMapFormatter =
            new Lazy<IMapFormatter>(() => Container.Resolve<IMapFormatter>());

        private static IContainer Container => _lazyContainer.Value;
        private static ISceneToMapConverter SceneToMapConverter => _lazySceneToMapConverter.Value;
        private static ISerializer Serializer => _lazySerializer.Value;
        private static IDeserializer Deserializer => _lazyDeserializer.Value;
        private static IMapFormatter MapFormatter => _lazyMapFormatter.Value;

        private const string MapEditorMenuPath = "Macerus/Map Editor";

        private const string NewMenuPath = MapEditorMenuPath + "/New";
        [MenuItem(NewMenuPath, false)]
        public static void NewMenu()
        {
            var dialogResult = EditorUtility.DisplayDialog(
                "New Map",
                "Are you sure you want to create a new map? You will lose ALL " +
                "changes to the current map.",
                "Yes, discard all changes!");
            if (!dialogResult)
            {
                return;
            }

            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            ClearCurrentMap(mapPrefab);
        }

        [MenuItem(NewMenuPath, true)]
        public static bool NewValidate() => CommonMenuValidation();

        private const string SaveAsMenuPath = MapEditorMenuPath + "/Save As...";
        [MenuItem(SaveAsMenuPath, false)]
        public static void SaveAsMenu()
        {
            var mapPathToSave = EditorUtility.SaveFilePanel("Save Map", "", "", "json");
            if (string.IsNullOrWhiteSpace(mapPathToSave))
            {
                return;
            }

            if (mapPathToSave.EndsWith(".objects.json", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = mapPathToSave.Replace(".objects.json", ".json");
                Debug.LogWarning(
                    $"Selected map object file '{mapPathToSave}' so attempting to save '{trimmed}'...");
                mapPathToSave = trimmed;
            }

            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            var gameObjects = SceneToMapConverter
                .ConvertGameObjects(mapPrefab)
                .ToArray();
            var mapTiles = SceneToMapConverter
                .ConvertTiles(mapPrefab)
                .ToArray();

            var mapGameOBjectsPath = mapPathToSave.Replace(".json", ".objects.json");
            using (var outputStream = new FileStream(mapGameOBjectsPath, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            {
                Serializer.Serialize(outputStream, gameObjects, Encoding.UTF8);
            }

            // FIXME: save the MAP out! (its more than just the tiles)
        }

        [MenuItem(SaveAsMenuPath, true)]
        public static bool SaveAsValidate() => CommonMenuValidation();

        private const string LoadMenuPath = MapEditorMenuPath + "/Load...";
        [MenuItem(LoadMenuPath, false)]
        public static void LoadMenu()
        {
            var mapPathToLoad = EditorUtility.OpenFilePanel("Load Map", "", "json");
            if (string.IsNullOrWhiteSpace(mapPathToLoad))
            {
                return;
            }

            if (mapPathToLoad.EndsWith(".objects.json", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = mapPathToLoad.Replace(".objects.json", ".json");
                Debug.LogWarning(
                    $"Selected map object file '{mapPathToLoad}' so attempting to load '{trimmed}'...");
                mapPathToLoad = trimmed;
            }

            Debug.Log($"Loading map '{mapPathToLoad}'...");
            IGameObject map;
            using (var inputStream = new FileStream(mapPathToLoad, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                map = Deserializer.Deserialize<IGameObject>(inputStream);
            }

            Debug.Log($"Loaded map '{mapPathToLoad}'.");

            var mapObjectsPathToLoad = mapPathToLoad.Replace(".json", ".objects.json");
            Debug.Log($"Loading map objects from '{mapObjectsPathToLoad}'...");
            IReadOnlyCollection<IGameObject> mapObjects;
            using (var inputStream = new FileStream(mapObjectsPathToLoad, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                mapObjects = Deserializer.Deserialize<IReadOnlyCollection<IGameObject>>(inputStream);
            }

            Debug.Log($"Loaded map objects from '{mapObjectsPathToLoad}'.");

            Debug.Log("Resetting current map...");
            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            ClearCurrentMap(mapPrefab);
            Debug.Log("Reset current map.");

            Debug.Log("Populating map...");
            MapFormatter.FormatMap(mapPrefab, map);
            MapFormatter.RemoveGameObjects(mapPrefab);
            SceneToMapConverter.ConvertGameObjects(mapPrefab, mapObjects);
            Debug.Log("Populated map.");
        }

        [MenuItem(LoadMenuPath, true)]
        public static bool LoadValidate() => CommonMenuValidation();

        private static bool CommonMenuValidation() => "MapEditor".Equals(
            SceneManager.GetActiveScene().name,
            StringComparison.OrdinalIgnoreCase);

        private static void ClearCurrentMap(IMapPrefab mapPrefab)
        {
            mapPrefab.Tilemap.ClearAllTiles();
            mapPrefab.Tilemap.ResizeBounds();
            foreach (var child in mapPrefab.GameObjectLayer.GetChildGameObjects())
            {
                UnityEngine.Object.DestroyImmediate(child);
            }
        }
    }
}
