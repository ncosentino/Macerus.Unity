using System.Linq;

using Assets.ContentCreator.MapEditor;
using Assets.Scripts.Autofac;
using Assets.Scripts.Plugins.Features.Maps;

using Autofac;

using Macerus.ContentCreator.MapEditor.Behaviors.Shared;

using NUnit.Framework;

using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Tests.EditorModeTests.ContentCreator.MapEditor
{
    public sealed class SceneToMapConverterTests
    {
        private static readonly IContainer _container;
        private static readonly ISceneToMapConverter _sceneToMapConverter;

        static SceneToMapConverterTests()
        {
            _container = new MacerusContainerBuilder().CreateContainer();
            _sceneToMapConverter = _container.Resolve<ISceneToMapConverter>();
        }

        [Test]
        public void ConvertGameObjects_EmptyMapGameObjectLayer_NoGameObjects()
        {
            var mapGameObject = new GameObject();

            var mapGameObjectLayer = new GameObject();
            mapGameObjectLayer.name = "GameObjectLayer";
            mapGameObjectLayer.transform.parent = mapGameObject.transform;

            var mapPrefab = new MapPrefab(mapGameObject);

            var gameObjects = _sceneToMapConverter
                .ConvertGameObjects(mapPrefab)
                .ToArray();

            Assert.IsEmpty(gameObjects);
        }

        [Test]
        public void ConvertGameObjects_SingleEmptyGameObject_MinimumRequiredBehaviors()
        {
            var mapGameObject = new GameObject();

            var mapGameObjectLayer = new GameObject();
            mapGameObjectLayer.name = "GameObjectLayer";
            mapGameObjectLayer.transform.parent = mapGameObject.transform;

            var unityGameObject = new GameObject();
            unityGameObject.transform.parent = mapGameObjectLayer.transform;
            unityGameObject.name = "UnityGameObject";

            var mapPrefab = new MapPrefab(mapGameObject);

            var gameObjects = _sceneToMapConverter
                .ConvertGameObjects(mapPrefab)
                .ToArray();

            Assert.AreEqual(1, gameObjects.Length);

            var gameObject = gameObjects[0];
            Assert.AreEqual(4, gameObject.Behaviors.Count);
            Assert.True(
                gameObject.Behaviors.Single(x => 
                x is EditorNameBehavior &&
                ((EditorNameBehavior)x).Name == "UnityGameObject") != null,
                $"Expecting to find a matching '{typeof(EditorNameBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                x is IReadOnlySizeBehavior &&
                ((IReadOnlySizeBehavior)x).Width == 1 &&
                ((IReadOnlySizeBehavior)x).Height == 1) != null,
                $"Expecting to find a matching '{typeof(IReadOnlySizeBehavior)}' on '{gameObject}'.");
            Assert.True(
                gameObject.Behaviors.Single(x =>
                x is IReadOnlyPositionBehavior &&
                ((IReadOnlyPositionBehavior)x).X == 0 &&
                ((IReadOnlyPositionBehavior)x).Y == 0) != null,
                $"Expecting to find a matching '{typeof(IReadOnlyPositionBehavior)}' on '{gameObject}'.");
            Assert.True(
            gameObject.Behaviors.Single(x =>
                x is IIdentifierBehavior &&
                ((IIdentifierBehavior)x).Id != null) != null,
                $"Expecting to find a matching '{typeof(IIdentifierBehavior)}' on '{gameObject}'.");
        }


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
    }
}