using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    public interface IPrefabCreator
    {
        TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object;
    }
}