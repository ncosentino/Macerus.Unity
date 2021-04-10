using System;
using System.Linq;

using Assets.Scripts.Unity.GameObjects;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapPrefab : IMapPrefab
    {
        private readonly Lazy<Tilemap> _lazyTileMap;
        private readonly Lazy<GameObject> _lazyGameObjectLayer;

        public MapPrefab(GameObject gameObject)
        {
            GameObject = gameObject;

            _lazyTileMap = new Lazy<Tilemap>(() =>
            {
                var tilemap = gameObject.GetComponentInChildren<Tilemap>();
                return tilemap;
            });
            _lazyGameObjectLayer = new Lazy<GameObject>(() =>
            {
                var tilemap = gameObject
                    .GetChildGameObjects(true)
                    .FirstOrDefault(x => x.name == "GameObjectLayer");
                return tilemap;
            });
        }

        public GameObject GameObject { get; }

        public Tilemap Tilemap => _lazyTileMap.Value;

        public GameObject GameObjectLayer => _lazyGameObjectLayer.Value;
    }
}