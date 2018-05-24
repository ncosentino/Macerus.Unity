using UnityEngine;

namespace Assets.Scripts.Unity.GameObjects
{
    public interface IObjectDestroyer
    {
        void Destroy(Object @object);
    }
}