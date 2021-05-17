using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EditorPrefabResourceBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public GameObject Prefab;
    }
}
