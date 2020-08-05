#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependenciesMenuItem
    {
        private static bool _isUpdateDependenciesDisabled;

        [MenuItem("Macerus Tools/Update Dependencies")]
        public static async void BuildGame()
        {
            _isUpdateDependenciesDisabled = true;
            try
            {
                await new DependencyUpdater().UpdateDependenciesAsync();
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
        }

        [MenuItem("Macerus Tools/Update Dependencies", true)]
        public static bool IsUpdateDependenciesEnabled() => !_isUpdateDependenciesDisabled;
    }
}
#endif