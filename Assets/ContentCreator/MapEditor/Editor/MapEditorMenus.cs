using System;
using System.Linq;

using Assets.Scripts.Autofac;

using Autofac;

using UnityEditor;

using UnityEngine;

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

        private const string SaveAsMenu = "Map Editor/Save As...";
        [MenuItem(SaveAsMenu, false)]
        public static async void SaveAsMenuAsync()
        {
            var sceneToMapConverter = Container.Resolve<ISceneToMapConverter>();

            var mapUnityGameObject = GameObject.Find("Map");
            var gameObjects = sceneToMapConverter
                .ConvertGameObjects(mapUnityGameObject)
                .ToArray();
            var mapTiles = sceneToMapConverter
                .ConvertTiles(mapUnityGameObject)
                .ToArray();
        }

        //[MenuItem(SaveAsMenu, true)]
        //public static bool SaveAsValidateAsync() => EditorApplication.isPlaying;
    }
}
