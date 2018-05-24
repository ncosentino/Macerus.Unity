using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    public interface IResourcePrefabLoader
    {
        TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object;
    }
}