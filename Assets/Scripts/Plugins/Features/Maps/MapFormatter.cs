using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Plugins.Features.FogOfWar;
using Assets.Scripts.Plugins.Features.GameObjects.Common;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources.Prefabs;

using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class MapFormatter : IMapFormatter
    {
        private const int LAYER_GRID_LINES = 10000;
        private const int LAYER_HOVER_SELECT = int.MaxValue;

        private readonly ITileLoader _tileLoader;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IUnityGameObjectRepository _unityGameObjectRepository;
        private readonly IPrefabCreator _prefabCreator;
        private readonly ILogger _logger;

        private bool _gridLinesEnabled;
        private IMapPrefab _mapPrefab;
        private int _maximumTileX;
        private int _maximumTileY;
        private int _minimumTileX;
        private int _minimumTileY;
        private Vector3Int? _lastHoverSelectTilePosition;

        public MapFormatter(
            ITileLoader tileLoader,
            IObjectDestroyer objectDestroyer,
            IUnityGameObjectRepository unityGameObjectRepository,
            IPrefabCreator prefabCreator,
            ILogger logger)
        {
            _tileLoader = tileLoader;
            _objectDestroyer = objectDestroyer;
            _unityGameObjectRepository = unityGameObjectRepository;
            _prefabCreator = prefabCreator;
            _logger = logger;
        }

        public void FormatMap(
            IMapPrefab mapPrefab,
            IGameObject map)
        {
            _mapPrefab = mapPrefab;
            _logger.Debug($"Formatting map object '{mapPrefab}' for '{map}'...");

            var parentMapObjectTransform = mapPrefab.GameObject.transform;

            int z = 0;
            _minimumTileX = int.MaxValue;
            _maximumTileX = int.MinValue;
            _minimumTileY = int.MaxValue;
            _maximumTileY = int.MinValue;

            mapPrefab.Tilemap.ClearAllTiles();
            foreach (var mapLayer in map.GetOnly<IMapLayersBehavior>().Layers)
            {
                foreach (var tile in mapLayer.Tiles)
                {
                    var tileResource = (ITileResourceBehavior)tile.Behaviors.First(x => x is ITileResourceBehavior);
                    var unityTile = _tileLoader.LoadTile(
                        tileResource.TilesetResourcePath,
                        tileResource.SpriteResourceName);
                    mapPrefab.Tilemap.SetTile(
                        new Vector3Int(tile.X, tile.Y, z),
                        unityTile);

                    _maximumTileX = Math.Max(_maximumTileX, tile.X);
                    _minimumTileX = Math.Min(_minimumTileX, tile.X);
                    _maximumTileY = Math.Max(_maximumTileY, tile.Y);
                    _minimumTileY = Math.Min(_minimumTileY, tile.Y);
                }
                
                z++;
            }

            var existingFogOfWar = parentMapObjectTransform.Find("FogOfWar");
            if (map.TryGetFirst<IHasFogOfWarBehavior>(out var fogOfWarBehavior))
            {
                var fogOfWarGameObject = _prefabCreator.Create<GameObject>("FogOfWar/FogOfWar");
                fogOfWarGameObject.name = "FogOfWar";
                fogOfWarGameObject.transform.SetParent(parentMapObjectTransform);

                var fogOfWarPrefab = new FogOfWarPrefab(fogOfWarGameObject);

                var fogWidthInterim = (int)Math.Max(
                    (int)Math.Abs(_minimumTileX),
                    (int)Math.Abs(_maximumTileX));
                var fogHeightInterim = (int)Math.Max(
                    (int)Math.Abs(_minimumTileY),
                    (int)Math.Abs(_maximumTileY));
                const float ROUND_UP_HALF_TILES = 1.5f;
                var fogSize = 2 * (Math.Max(fogWidthInterim, fogHeightInterim) + ROUND_UP_HALF_TILES);

                fogOfWarPrefab.FogMainTextureTransform.sizeDelta = new Vector2(fogSize, fogSize);
                fogOfWarPrefab.FogCameraMain.orthographicSize = fogSize / 2;
            }

            if (existingFogOfWar != null)
            {
                // NOTE: we *MUST* remove the parent reference because
                // destruction doesn't actually occur until the next engine
                // tick. as a result, people can accidentally lookup these
                // objects before the next tick.
                existingFogOfWar.parent = null;
                _objectDestroyer.Destroy(existingFogOfWar.gameObject);
            }

            // set these back (if needed) because we just obliterated all the
            // tiles by formatting the tile map
            ToggleGridLines(_gridLinesEnabled, false);
            
            mapPrefab.Tilemap.RefreshAllTiles();
            _logger.Debug($"Formatted map object '{mapPrefab}' for '{map}'.");
        }

        public void HoverSelectTile(Vector2Int? position)
        {
            if (_lastHoverSelectTilePosition.HasValue)
            {
                _mapPrefab.Tilemap.SetTile(
                    _lastHoverSelectTilePosition.Value,
                    null);
            }

            _lastHoverSelectTilePosition = position == null
                ? (Vector3Int?)null
                : new Vector3Int(
                    position.Value.x,
                    position.Value.y,
                    LAYER_HOVER_SELECT);

            if (position.HasValue)
            {
                var unityTile = _tileLoader.LoadTile(
                    "mapping/tilesets/",
                    "tile-hover-select-overlay");
                _mapPrefab.Tilemap.SetTile(
                    _lastHoverSelectTilePosition.Value,
                    unityTile);
            }
        }

        public void ToggleGridLines(bool enabled) =>
            ToggleGridLines(enabled, true);

        public void RemoveGameObjects(
            GameObject mapObject,
            params IIdentifier[] gameObjectIds) => RemoveGameObjects(
                mapObject,
                (IEnumerable<IIdentifier>) gameObjectIds);

        public void RemoveGameObjects(
            GameObject mapObject,
            IEnumerable<IIdentifier> gameObjectIds)
        {
            var set = new HashSet<IIdentifier>(gameObjectIds);
            var gameObjectLayerObject = _mapPrefab.GameObjectLayer;

            foreach (var toRemove in gameObjectLayerObject
                .GetComponentsInChildren<IdentifierBehaviour>() // FIXME: this is a hack to require concrete type
                .Where(x => set.Contains(x.Id))
                .Select(x => x.gameObject))
            {
                _objectDestroyer.Destroy(toRemove);
            }
        }

        public void AddGameObjects(
            GameObject mapObject,
            params IGameObject[] gameObjects) => AddGameObjects(
            mapObject,
            (IEnumerable<IGameObject>)gameObjects);

        public void AddGameObjects(
            GameObject mapObject,
            IEnumerable<IGameObject> gameObjects)
        {
            var gameObjectLayerObject = _mapPrefab.GameObjectLayer;
            foreach (var gameObject in gameObjects)
            {
                _logger.Debug($"Transforming game object '{gameObject}'...");
                var unityGameObject = _unityGameObjectRepository.Create(gameObject);

                // add the game object to the correct parent
                unityGameObject.transform.parent = gameObjectLayerObject.transform;
                _logger.Debug($"Adding unity game object '{unityGameObject}' to '{gameObjectLayerObject}'...");
            }
        }

        private void ToggleGridLines(
            bool enabled,
            bool forceRefresh)
        {
            _gridLinesEnabled = enabled;

            // may not have been loaded yet
            if (_mapPrefab == null || _mapPrefab.Tilemap == null)
            {
                return;
            }

            for (int i = _minimumTileX; i <= _maximumTileX; i++)
            {
                for (int j = _minimumTileY; j <= _maximumTileY; j++)
                {
                    var unityTile = enabled
                        ? _tileLoader.LoadTile(
                            "mapping/tilesets/",
                            "tile-border-overlay")
                        : null;
                    _mapPrefab.Tilemap.SetTile(
                        new Vector3Int(i, j, LAYER_GRID_LINES),
                        unityTile);
                }
            }

            if (forceRefresh)
            {
                _mapPrefab.Tilemap.RefreshAllTiles();
            }
        }
    }
}