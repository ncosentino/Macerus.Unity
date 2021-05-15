using System;
using System.Collections.Generic;
using System.Linq;

using Assets.ContentCreator.MapEditor.Behaviours;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor
{
    public interface ISceneToMapConverter
    {
        void XXX(GameObject mapGameObject);
    }

    public sealed class SceneToMapConverter : ISceneToMapConverter
    {
        private Lazy<IGameObjectConverter> _gameObjectConverter;

        public SceneToMapConverter(Lazy<IGameObjectConverter> gameObjectConverter)
        {
            _gameObjectConverter = gameObjectConverter;
        }

        private IGameObjectConverter GameObjectConverter => _gameObjectConverter.Value;

        public void XXX(GameObject mapGameObject)
        {
            mapGameObject = new GameObject(); // FIXME: just for testing
            mapGameObject.name = "Fake Map";
            var firstGameObjectLayer = new GameObject();
            firstGameObjectLayer.name = "First Game Object Layer";
            firstGameObjectLayer.transform.parent = mapGameObject.transform;

            var templateSpawner = new GameObject();
            templateSpawner.name = "Template Spawner";
            templateSpawner.transform.parent = firstGameObjectLayer.transform;
            templateSpawner.AddComponent<IdentifierBehaviour>();
            templateSpawner.AddComponent<TypeIdentifierBehaviour>().TypeId = "Static";
            //testyboi.AddComponent<TemplateIdentifierBehaviour>().TemplateId = "Skeleton";
            //testyboi.AddComponent<PrefabResourceBehaviour>().Prefab = Resources.Load<GameObject>("mapping/prefabs/actors/actor");
            //testyboi.AddComponent<DynamicAnimationBehaviour>();
            templateSpawner.AddComponent<TriggerOnCombatEndBehaviour>();

            var spawnTemplateProperties = new GameObject();
            spawnTemplateProperties.name = "SpawnTemplatePropertiesBehavior";
            spawnTemplateProperties.transform.parent = templateSpawner.transform;
            spawnTemplateProperties.AddComponent<IdentifierBehaviour>();
            spawnTemplateProperties.AddComponent<TypeIdentifierBehaviour>().TypeId = "Static";
            // FIXME: how to set x,y,width,height to = parent?
            var doorBehaviour = spawnTemplateProperties.AddComponent<DoorBehaviour>();
            doorBehaviour.AutomaticInteraction = false;
            doorBehaviour.TransitionToMapId = null; // "swamp";
            doorBehaviour.HasPositionTransition = false;
            doorBehaviour.TransitionToX = 0; //40;
            doorBehaviour.TransitionToY = 0;// -16;

            //<object id="18" name="tiled specific name" type="tiled specific type" x="1600" y="-600" width="32" height="32">
            //    <property name="typeId" value="static"/>
            //    <property name="templateId" value=""/>
            //    <property name="prefabId" value="static/rectangulartrigger"/>
            //    <property name="TriggerOnCombatEnd" />
            //    <property name="SpawnTemplateProperties">
            //      <property name="typeId" value="static"/>
            //      <property name="templateId" value="??this is a door but proof we don't require this??"/>
            //      <property name="prefabId" value="static/encounterspawn"/>
            //      <property name="x" value="50"/>
            //      <property name="y" value="-20"/>
            //      <property name="width" value="1"/>
            //      <property name="height" value="1"/>
            //      <property name="DoorBehavior">
            //        <property name="AutomaticInteraction" value="False"/>
            //        <property name="TransitionToMapId" value="swamp"/>
            //        <property name="TransitionToX" value="40"/>
            //        <property name="TransitionToY" value="-16"/>
            //      </property>
            //    </property>
            //  </object>

            // FIXME: we want to go through designated object layers but not
            // into the objects themselves on this outer loop
            foreach (var gameObjectLayer in mapGameObject.GetChildGameObjects(true))
            {
                foreach (var unityGameObject in gameObjectLayer.GetChildGameObjects(true))
                {
                    var gameObject = GameObjectConverter.Convert(unityGameObject).ToArray();
                    var rehydratedUnityGameObject = GameObjectConverter.Convert(gameObject);
                }
            }
        }
    }
}
