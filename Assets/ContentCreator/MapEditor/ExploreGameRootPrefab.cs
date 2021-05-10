using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Maps;
using Assets.Scripts.Unity.GameObjects;

using NexusLabs.Contracts;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class ExploreGameRootPrefab : IExploreGameRootPrefab
    {
        private readonly Lazy<IMapPrefab> _lazyMapPrefab;

        public ExploreGameRootPrefab(GameObject gameObject)
        {
            GameObject = gameObject;
            _lazyMapPrefab = new Lazy<IMapPrefab>(() =>
            {
                var mapGameObject = gameObject
                    .GetChildGameObjects(false)
                    .FirstOrDefault(x => x.name == "Map");
                Contract.RequiresNotNull(
                    mapGameObject,
                    "Could not find the map game object. Did you attempt to " +
                    "access this before the map was created?");
                var prefab = new MapPrefab(mapGameObject);
                return prefab;
            });
        }

        public GameObject GameObject { get; }

        public IMapPrefab MapPrefab => _lazyMapPrefab.Value;
    }
}
