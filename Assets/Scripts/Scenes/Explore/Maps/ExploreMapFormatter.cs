using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Scenes.Explore.Maps.GameObjects;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Resources;
using Macerus.Api.Behaviors;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Game.Interface.Mapping;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Maps
{
    public sealed class ExploreMapFormatter : IExploreMapFormatter
    {
        private const string GAME_OBJECT_LAYER_NAME = "Game Objects";

        private readonly ITileLoader _tileLoader;
        private readonly IObjectDestroyer _objectDestroyer;
        private readonly IResourcePrefabLoader _resourcePrefabLoader;

        public ExploreMapFormatter(
            ITileLoader tileLoader,
            IObjectDestroyer objectDestroyer,
            IResourcePrefabLoader resourcePrefabLoader)
        {
            _tileLoader = tileLoader;
            _objectDestroyer = objectDestroyer;
            _resourcePrefabLoader = resourcePrefabLoader;
        }

        public void FormatMap(
            GameObject mapObject,
            IMap map)
        {
            Debug.Log($"Formatting map object '{mapObject}' for '{map}'...");

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

            Debug.Log($"Formatted map object '{mapObject}' for '{map}'.");
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
                .GetComponentsInChildren<IdentifierBehaviour>()
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
                // TODO: load the correct resource based on some behavior
                var relativePrefabResourcePath = "Mapping/Prefabs/PlayerPlaceholder";
                var unityGameObject = _resourcePrefabLoader.Create<GameObject>(relativePrefabResourcePath);

                // set an ID on the game object
                var gameObjectId = gameObject
                    .Behaviors
                    .Get<IIdentifierBehavior>()
                    .Single()
                    .Id;
                unityGameObject.GetRequiredComponent<IIdentifierBehaviour>().Id = gameObjectId;
                unityGameObject.name = $"GameObject {gameObjectId}";

                // add the game object to the correct parent
                unityGameObject.transform.parent = gameObjectLayerObject.transform;

                // move the game object to the right spot
                var worldLocation = gameObject
                    .Behaviors
                    .Get<IWorldLocationBehavior>()
                    .Single();
                unityGameObject.transform.position = new Vector3(
                    (float)worldLocation.X,
                    (float)worldLocation.Y,
                    -1);
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