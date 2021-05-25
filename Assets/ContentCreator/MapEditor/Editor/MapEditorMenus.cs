using System;
using System.IO;

using Assets.Scripts.Autofac;

using Autofac;

using UnityEditor;

using UnityEngine.SceneManagement;

namespace Assets.ContentCreator.MapEditor.Editor
{
    public sealed partial class MapEditorMenus
    {
        private static Lazy<IContainer> _lazyContainer = new
            Lazy<IContainer>(() => new MacerusContainerBuilder().CreateContainer());
        private static Lazy<IMapEditor> _lazyMapEditor =
            new Lazy<IMapEditor>(() => Container.Resolve<IMapEditor>());

        private static IContainer Container => _lazyContainer.Value;
        private static IMapEditor MapEditor => _lazyMapEditor.Value;

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

            MapEditor.ClearCurrentMap();
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

            MapEditor.SaveMap(
                mapPathToSave,
                path => new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite));
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

            MapEditor.LoadMap(
                mapPathToLoad,
                path => new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

        [MenuItem(LoadMenuPath, true)]
        public static bool LoadValidate() => CommonMenuValidation();

        private static bool CommonMenuValidation() => "MapEditor".Equals(
            SceneManager.GetActiveScene().name,
            StringComparison.OrdinalIgnoreCase);        
    }
}
