using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Tilemaps;

using Macerus.Plugins.Features.Mapping;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;

using UnityEditor;

using UnityEngine;
using UnityEngine.Tilemaps;

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

        public IEnumerable<IGameObject> ConvertTiles(GameObject mapGameObject)
        {
            foreach (var tileMap in mapGameObject.GetChildComponents<Tilemap>(true))
            {
                var tiles = tileMap.GetAllTiles();
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
        }

        public IEnumerable<IGameObject> ConvertGameObjects(GameObject mapGameObject)
        {
            foreach (var gameObjectLayer in mapGameObject.GetChildGameObjects(true))
            {
                foreach (var unityGameObject in gameObjectLayer.GetChildGameObjects(true))
                {
                    var behaviors = GameObjectConverter.Convert(unityGameObject).ToArray();
                    var gameObject = GameObjectFactory.Create(behaviors);
                    yield return gameObject;
                }
            }
        }
    }
}
