using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Unity.Tilemaps;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Plugins.Features.Mapping.Default;

using UnityEditor;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.ContentCreator.MapEditor
{
    public sealed class SceneToMapConverter : ISceneToMapConverter
    {
        private Lazy<IGameObjectConverter> _gameObjectConverter;

        public SceneToMapConverter(Lazy<IGameObjectConverter> gameObjectConverter)
        {
            _gameObjectConverter = gameObjectConverter;
        }

        private IGameObjectConverter GameObjectConverter => _gameObjectConverter.Value;

        public void Convert(GameObject mapGameObject)
        {
            //mapGameObject = new GameObject(); // FIXME: just for testing
            //mapGameObject.name = "Fake Map";
            //var firstGameObjectLayer = new GameObject();
            //firstGameObjectLayer.name = "First Game Object Layer";
            //firstGameObjectLayer.transform.parent = mapGameObject.transform;

            //var templateSpawner = new GameObject();
            //templateSpawner.name = "Template Spawner";
            //templateSpawner.transform.parent = firstGameObjectLayer.transform;
            //templateSpawner.AddComponent<IdentifierBehaviour>();
            //templateSpawner.AddComponent<TypeIdentifierBehaviour>().TypeId = "Static";
            ////testyboi.AddComponent<TemplateIdentifierBehaviour>().TemplateId = "Skeleton";
            ////testyboi.AddComponent<PrefabResourceBehaviour>().Prefab = Resources.Load<GameObject>("mapping/prefabs/actors/actor");
            ////testyboi.AddComponent<DynamicAnimationBehaviour>();
            //templateSpawner.AddComponent<TriggerOnCombatEndBehaviour>();

            //var spawnTemplateProperties = new GameObject();
            //spawnTemplateProperties.name = "SpawnTemplatePropertiesBehavior";
            //spawnTemplateProperties.transform.parent = templateSpawner.transform;
            //spawnTemplateProperties.AddComponent<IdentifierBehaviour>();
            //spawnTemplateProperties.AddComponent<TypeIdentifierBehaviour>().TypeId = "Static";
            //// FIXME: how to set x,y,width,height to = parent?
            //var doorBehaviour = spawnTemplateProperties.AddComponent<DoorBehaviour>();
            //doorBehaviour.AutomaticInteraction = false;
            //doorBehaviour.TransitionToMapId = null; // "swamp";
            //doorBehaviour.HasPositionTransition = false;
            //doorBehaviour.TransitionToX = 0; //40;
            //doorBehaviour.TransitionToY = 0;// -16;
            foreach (var gameObjectLayer in mapGameObject.GetChildGameObjects(true))
            {
                var tileMap = gameObjectLayer.GetComponent<Tilemap>();
                if (tileMap != null)
                {
                    var tiles = tileMap.GetAllTiles();
                    foreach (var tile in tiles)
                    {
                        var spriteAssetPath = AssetDatabase.GetAssetPath(tile.Sprite);
                        spriteAssetPath = spriteAssetPath
                            .Substring("Assets/Resources/".Length) // we need a relative path
                            .Replace(".png", string.Empty) // we don't want the extension
                            + "/" + tile.Sprite.name;
                        var mapTile = new MapTile(
                            tile.X,
                            tile.Y,
                            new IBehavior[0]);
                    }
                }

                foreach (var unityGameObject in gameObjectLayer.GetChildGameObjects(true))
                {
                    var gameObject = GameObjectConverter.Convert(unityGameObject).ToArray();
                    var rehydratedUnityGameObject = GameObjectConverter.Convert(gameObject);
                }
            }
        }
    }
}
