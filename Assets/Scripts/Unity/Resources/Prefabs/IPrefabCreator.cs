using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Prefabs
{
    public interface IPrefabCreator
    {
        TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object;

        TPrefab CreatePrefab<TPrefab>(string relativePrefabPathWithinResources)
            where TPrefab : IPrefab;
    }
}