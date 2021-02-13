using System;
using System.Collections.Generic;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.Scripts.Unity.Resources.Prefabs
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
            where TPrefab : IPrefab => Register(
                typeof(TPrefab),
                factory);

        public void Register(Type type, PrefabFactoryDelegate factory)
        {
            Contract.Requires(
                typeof(IPrefab).IsAssignableFrom(type),
                $"'{type}' must inherit from '{typeof(IPrefab)}'.");

            if (_mapping.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"Cannot register for type '{type}' because it has already " +
                    $"been registered.");
            }

            _mapping[type] = factory;
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
