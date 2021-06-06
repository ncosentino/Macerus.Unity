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
                if (@object is GameObject gameObject)
                {
                    gameObject.transform.SetParent(null);
                }
            }
            else
            {
                Object.DestroyImmediate(@object);
            }
#else
            Object.Destroy(@object);
            if (@object is GameObject gameObject)
            {
                gameObject.transform.SetParent(null);
            }
#endif
        }
    }
}
