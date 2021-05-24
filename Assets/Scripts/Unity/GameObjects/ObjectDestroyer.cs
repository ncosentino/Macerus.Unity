using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public sealed class ObjectDestroyer : IObjectDestroyer
    {
        public void Destroy(Object @object)
        {
#if UNITY_EDITOR
            Object.DestroyImmediate(@object);
#else
            Object.Destroy(@object);
            @object.transform.parent = null;
#endif
        }
    }
}
