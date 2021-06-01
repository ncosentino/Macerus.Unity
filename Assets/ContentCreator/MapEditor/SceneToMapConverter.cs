using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;
using Assets.Scripts.Plugins.Features.Maps.Api;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Tilemaps;

using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;

using UnityEditor;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class SceneToMapConverter : ISceneToMapConverter
    {
        private readonly Lazy<IGameObjectFactory> _gameObjectFactory;
        private readonly Lazy<IGameObjectConverter> _gameObjectConverter;

        public SceneToMapConverter(
            Lazy<IGameObjectFactory> gameObjectFactory,
            Lazy<IGameObjectConverter> gameObjectConverter)
        {
            _gameObjectFactory = gameObjectFactory;
            _gameObjectConverter = gameObjectConverter;
        }

        private IGameObjectConverter GameObjectConverter => _gameObjectConverter.Value;

        private IGameObjectFactory GameObjectFactory => _gameObjectFactory.Value;

        public IEnumerable<IGameObject> ConvertTiles(IMapPrefab mapPrefab)
        {
            var tiles = mapPrefab.Tilemap.GetAllTiles();
            foreach (var tile in tiles)
            {
                var spriteAssetPath = AssetDatabase.GetAssetPath(tile.Sprite);
                spriteAssetPath = spriteAssetPath
                    .Substring(spriteAssetPath.IndexOf("/Resources/") + "/Resources/".Length) // we need a relative path
                    .Replace(".png", string.Empty); // we don't want the extension
                var mapTile = GameObjectFactory.Create(new IBehavior[]
                {
                    new PositionBehavior(tile.X, tile.Y),
                    new TileResourceBehavior(
                        spriteAssetPath,
                        tile.Sprite.name)
                });
                yield return mapTile;
            }
        }

        public IEnumerable<IGameObject> ConvertGameObjects(IMapPrefab mapPrefab)
        {
            foreach (var unityGameObject in mapPrefab.GameObjectLayer.GetChildGameObjects(true))
            {
                var behaviors = GameObjectConverter.Convert(unityGameObject).ToArray();
                var gameObject = GameObjectFactory.Create(behaviors);
                yield return gameObject;
            }
        }

        public void ConvertGameObjects(
            IMapPrefab mapPrefab,
            IEnumerable<IGameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                var unityGameObject = GameObjectConverter.Convert(gameObject.Behaviors);
                unityGameObject.transform.SetParent(mapPrefab.GameObjectLayer.transform);
            }
        }
    }
}
