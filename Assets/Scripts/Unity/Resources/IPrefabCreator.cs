using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    public interface IPrefabCreator
    {
        TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object;

        TPrefab CreatePrefab<TPrefab>(string relativePrefabPathWithinResources)
            where TPrefab : IPrefab;
    }

    public delegate IPrefab PrefabFactoryDelegate(GameObject gameObject);

    public interface IPrefabCreatorRegistrar
    {
        void Register<TPrefab>(PrefabFactoryDelegate factory)
            where TPrefab : IPrefab;
    }
}