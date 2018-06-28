#if UNITY_EDITOR
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependenciesMenuItem
    {
        [MenuItem("Macerus Tools/Update Dependencies")]
        public static void BuildGame()
        {
            new DependencyUpdater()
                .UpdateDependenciesAsync()
                .ContinueWith(
                (a, __) =>
                {
                    Debug.LogError("An exception was caught while updating dependencies.");
                    Debug.LogException(a.Exception);
                },
                null,
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
#endif