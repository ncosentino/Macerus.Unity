using System;
using System.Threading.Tasks;

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependenciesMenuItem
    {
        private static bool _isUpdateDependenciesDisabled;

        private const string DependenciesMenuPath = "Macerus/Dependencies";

        private const string UpdateAndBuildMenu = DependenciesMenuPath + "/Build+Copy Dependencies";
        [MenuItem(UpdateAndBuildMenu)]
        public static async void UpdateAndBuildDependencies()
        {
            await UpdateDependencies(true);
        }

        [MenuItem(UpdateAndBuildMenu, true)]
        public static bool IsUpdateAndBuildDependenciesEnabled() =>
            !_isUpdateDependenciesDisabled &&
            !EditorApplication.isPlaying;

        private const string OnlyUpdateMenu = DependenciesMenuPath + "/Only Copy Dependencies";
        [MenuItem(OnlyUpdateMenu)]
        public static async void OnlyUpdateDependencies()
        {
            await UpdateDependencies(false);
        }

        [MenuItem(OnlyUpdateMenu, true)]
        public static bool IsOnlyUpdateDependenciesEnabled() =>
            !_isUpdateDependenciesDisabled &&
            !EditorApplication.isPlaying;

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