using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class PrefabResourceBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public GameObject Prefab;
    }
}
