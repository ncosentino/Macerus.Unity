using System;
using System.Linq;

using Assets.Scripts.Autofac;
using Assets.Scripts.Plugins.Features.Maps;
using Assets.Scripts.Unity.GameObjects;

using Autofac;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.ContentCreator.MapEditor.Editor
{
    public sealed class MapEditorMenus
    {
        //private static Lazy<ISceneToMapConverter> _lazySceneToMapConverter =
        //    new Lazy<ISceneToMapConverter>(() => GameDependencyBehaviour
        //        .Container
        //        .Resolve<ISceneToMapConverter>());

        private static Lazy<IContainer> _lazyContainer = new
            Lazy<IContainer>(() => new MacerusContainerBuilder().CreateContainer());
        private static IContainer Container => _lazyContainer.Value;

        private const string MapEditorMenuPath = "Macerus/Map Editor";

        private const string SaveAsMenuPath = MapEditorMenuPath + "/Save As...";
        [MenuItem(SaveAsMenuPath, false)]
        public static void SaveAsMenu()
        {
            var sceneToMapConverter = Container.Resolve<ISceneToMapConverter>();

            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            var gameObjects = sceneToMapConverter
                .ConvertGameObjects(mapPrefab.GameObject)
                .ToArray();
            var mapTiles = sceneToMapConverter
                .ConvertTiles(mapPrefab.GameObject)
                .ToArray();
        }

        [MenuItem(SaveAsMenuPath, true)]
        public static bool SaveAsValidate() => "MapEditor".Equals(
            SceneManager.GetActiveScene().name,
            StringComparison.OrdinalIgnoreCase);

        private const string LoadMenuPath = MapEditorMenuPath + "/Load...";
        [MenuItem(LoadMenuPath, false)]
        public static void LoadMenu()
        {
            var sceneToMapConverter = Container.Resolve<ISceneToMapConverter>();

            var mapPathToLoad = EditorUtility.OpenFilePanel("Load Map", "", "json");

            var mapUnityGameObject = GameObject.Find("Map");
            var mapPrefab = new MapPrefab(mapUnityGameObject);
            
            mapPrefab.Tilemap.ClearAllTiles();
            mapPrefab.Tilemap.ResizeBounds();
            foreach (var child in mapPrefab.GameObjectLayer.GetChildGameObjects())
            {
                UnityEngine.Object.DestroyImmediate(child);
            }

            // FIXME: load the map!
        }

        [MenuItem(LoadMenuPath, true)]
        public static bool LoadValidate() => "MapEditor".Equals(
            SceneManager.GetActiveScene().name,
            StringComparison.OrdinalIgnoreCase);
    }
}
