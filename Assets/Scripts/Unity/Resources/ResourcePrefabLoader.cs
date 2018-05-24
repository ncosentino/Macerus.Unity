using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    using Resources = UnityEngine.Resources;

    public sealed class ResourcePrefabLoader : IResourcePrefabLoader
    {
        public TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object
        {
            var resource = Resources.Load(
                relativePrefabPathWithinResources,
                typeof(TGameObject));
            var uncasted = Object.Instantiate(resource);
            var casted = (TGameObject)uncasted;
            return casted;
        }
    }
}
