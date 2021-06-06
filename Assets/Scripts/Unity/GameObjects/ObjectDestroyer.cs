using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public sealed class ObjectDestroyer : IObjectDestroyer
    {
        public void Destroy(Object @object)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Object.Destroy(@object);
            }
            else
            {
                Object.DestroyImmediate(@object);
            }
#else
            Object.Destroy(@object);
#endif
        }
    }
}
