using System;
using System.Linq;

using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Plugins.Features.Maps
{
    public sealed class MapPrefab : IMapPrefab
    {
        private readonly Lazy<Tilemap> _lazyTileMap;
        private readonly Lazy<Tilemap> _lazyWalkIndicatorTileMap;
        private readonly Lazy<GameObject> _lazyGameObjectLayer;

        public MapPrefab(GameObject gameObject)
        {
            GameObject = gameObject;

            _lazyTileMap = new Lazy<Tilemap>(() =>
            {
                var tilemap = gameObject.GetComponentsInChildren<Tilemap>().First(x => x.gameObject.name == "TileMap");
                return tilemap;
            });
            _lazyWalkIndicatorTileMap = new Lazy<Tilemap>(() =>
            {
                var tilemap = gameObject.GetComponentsInChildren<Tilemap>().First(x => x.gameObject.name == "WalkIndicatorTileMap");
                return tilemap;
            });
            _lazyGameObjectLayer = new Lazy<GameObject>(() =>
            {
                var gameObjectLayer = gameObject
                    .GetChildGameObjects(true)
                    .FirstOrDefault(x => x.name == "GameObjectLayer");
                return gameObjectLayer;
            });
        }

        public GameObject GameObject { get; }

        public Tilemap Tilemap => _lazyTileMap.Value;

        public Tilemap WalkIndicatorTilemap => _lazyWalkIndicatorTileMap.Value;

        public GameObject GameObjectLayer => _lazyGameObjectLayer.Value;
    }
}