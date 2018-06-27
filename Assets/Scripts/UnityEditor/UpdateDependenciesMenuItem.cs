#if UNITY_EDITOR
using UnityEditor;

namespace Assets.Scripts.UnityEditor
{
    public sealed class UpdateDependenciesMenuItem
    {
        [MenuItem("Macerus Tools/Update Dependencies")]
        public static void BuildGame()
        {
            new DependencyUpdater().UpdateDependenciesAsync();
        }
    }
}
#endif