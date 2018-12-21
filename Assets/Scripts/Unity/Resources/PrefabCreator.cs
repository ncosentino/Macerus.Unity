using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Unity.Resources
{
    using Object = UnityEngine.Object;

    public sealed class PrefabCreator :
        IPrefabCreator,
        IPrefabCreatorRegistrar
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly Dictionary<Type, PrefabFactoryDelegate> _mapping;

        public PrefabCreator(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            _mapping = new Dictionary<Type, PrefabFactoryDelegate>();
        }

        public TGameObject Create<TGameObject>(string relativePrefabPathWithinResources)
            where TGameObject : Object
        {
            var resource = _resourceLoader.Load<TGameObject>(relativePrefabPathWithinResources);
            var instantiated = Object.Instantiate(resource);
            return instantiated;
        }

        public void Register<TPrefab>(PrefabFactoryDelegate factory)
            where TPrefab : IPrefab
        {
            // explicit add so we explode if one exists
            _mapping.Add(typeof(TPrefab), factory);
        }

        public TPrefab CreatePrefab<TPrefab>(string relativePrefabPathWithinResources)
            where TPrefab : IPrefab
        {
            PrefabFactoryDelegate factory;
            if (!_mapping.TryGetValue(
                typeof(TPrefab),
                out factory))
            {
                throw new InvalidOperationException(
                    $"There is no factory function registered for prefab type '{typeof(TPrefab)}'.");
            }

            var gameObject = Create<GameObject>(relativePrefabPathWithinResources);
            var prefab = factory.Invoke(gameObject);
            var casted = (TPrefab)prefab;
            return casted;
        }
    }
}
