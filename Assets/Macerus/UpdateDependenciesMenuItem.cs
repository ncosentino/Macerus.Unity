#if UNITY_EDITOR
using System;
using System.Threading.Tasks;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependenciesMenuItem
    {
        private static bool _isUpdateDependenciesDisabled;

        private const string UpdateAndBuildMenu = "Macerus Tools/Update and Build Dependencies";
        [MenuItem(UpdateAndBuildMenu)]
        public static async void UpdateAndBuildDependencies()
        {
            await UpdateDependencies(true);
        }

        [MenuItem(UpdateAndBuildMenu, true)]
        public static bool IsUpdateAndBuildDependenciesEnabled() => !_isUpdateDependenciesDisabled;

        private const string OnlyUpdateMenu = "Macerus Tools/Only Update Dependencies";
        [MenuItem(OnlyUpdateMenu)]
        public static async void OnlyUpdateDependencies()
        {
            await UpdateDependencies(false);
        }

        [MenuItem(OnlyUpdateMenu, true)]
        public static bool IsOnlyUpdateDependenciesEnabled() => !_isUpdateDependenciesDisabled;

        private static async Task UpdateDependencies(bool buildDependencies)
        {
            _isUpdateDependenciesDisabled = true;
            try
            {
                await new DependencyUpdater().UpdateDependenciesAsync(buildDependencies);
            }
            catch (Exception ex)
            {
                Debug.LogError("An exception was caught while updating dependencies.");
                Debug.LogException(ex);
            }
            finally
            {
                _isUpdateDependenciesDisabled = false;
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }
    }
}
#endif