using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Plugins.Features.GameObjects.Common;
using Assets.Scripts.Plugins.Features.GameObjects.Common.Api;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Maps
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class MapFormatter : IMapFormatter
    {
        private const string GAME_OBJECT_LAYER_NAME = "Game Objects";

        private readonly ITileLoader _tileLoader;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IUnityGameObjectRepository _unityGameObjectRepository;
        private readonly ILogger _logger;

        public MapFormatter(
            ITileLoader tileLoader,
            IObjectDestroyer objectDestroyer,
            IUnityGameObjectRepository unityGameObjectRepository,
            ILogger logger)
        {
            _tileLoader = tileLoader;
            _objectDestroyer = objectDestroyer;
            _unityGameObjectRepository = unityGameObjectRepository;
            _logger = logger;
        }

        public void FormatMap(
            GameObject mapObject,
            IMap map)
        {
            _logger.Debug($"Formatting map object '{mapObject}' for '{map}'...");

            var parentMapObjectTransform = mapObject.transform;

            foreach (Transform child in parentMapObjectTransform)
            {
                _objectDestroyer.Destroy(child.gameObject);
            }

            int z = 0;
            foreach (var mapLayer in map.Layers)
            {
                var mapLayerObject = new GameObject($"{mapLayer.Name}");
                mapLayerObject.transform.parent = parentMapObjectTransform;

                foreach (var tile in mapLayer.Tiles)
                {
                    var tileResource = (ITileResourceComponent)tile.Components.First(x => x is ITileResourceComponent);
                    var tileObject = _tileLoader.CreateTile(
                        tile.X,
                        tile.Y,
                        z,
                        tileResource.TilesetResourcePath,
                        tileResource.SpriteResourceName);
                    tileObject.transform.parent = mapLayerObject.transform;
                }
                
                z++;
            }

            // FIXME: probably want multiple layers and stuff for this
            var gameObjectLayerObject = new GameObject(GAME_OBJECT_LAYER_NAME);
            gameObjectLayerObject.transform.parent = parentMapObjectTransform;

            _logger.Debug($"Formatted map object '{mapObject}' for '{map}'.");
        }

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
            var gameObjectLayerObject = FindGameObjectLayer(mapObject);

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
            var gameObjectLayerObject = FindGameObjectLayer(mapObject);

            foreach (var gameObject in gameObjects)
            {
                _logger.Debug($"Transforming game object '{gameObject}'...");
                var unityGameObject = _unityGameObjectRepository.Create(gameObject);

                // add the game object to the correct parent
                unityGameObject.transform.parent = gameObjectLayerObject.transform;
                _logger.Debug($"Adding unity game object '{unityGameObject}' to '{gameObjectLayerObject}'...");
            }
        }

        private GameObject FindGameObjectLayer(GameObject mapObject)
        {
            var gameObjectLayer = mapObject
                .GetChildGameObjects()
                .First(x => x.name == GAME_OBJECT_LAYER_NAME);
            return gameObjectLayer;
        }
    }
}