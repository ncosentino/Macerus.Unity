using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public sealed class ObjectDestroyer : IObjectDestroyer
    {
        public void Destroy(Object @object)
        {
            Object.Destroy(@object);
        }
    }
}
