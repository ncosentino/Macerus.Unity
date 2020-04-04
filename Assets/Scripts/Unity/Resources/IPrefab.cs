using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    public interface IPrefab
    {
        GameObject GameObject { get; }
    }

    public static class IPrefabExtensionMethods
    {
        public static TComponent AddComponent<TComponent>(this IPrefab prefab)
            where TComponent : MonoBehaviour
        {
            var component = prefab.GameObject.AddComponent<TComponent>();
            return component;
        }

        public static void SetParent(
            this IPrefab prefab,
            GameObject parentGameObject)
        {
            prefab.SetParent(parentGameObject.transform);
        }

        public static void SetParent(
            this IPrefab prefab,
            Transform parentTransform)
        {
            prefab
                .GameObject
                .transform
                .SetParent(parentTransform, false);
        }
    }
}