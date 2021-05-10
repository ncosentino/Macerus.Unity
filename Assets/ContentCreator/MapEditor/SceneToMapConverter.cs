using System.Collections.Generic;

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
        private IBehaviorConverterFacade _behaviorConverterFacade;

        public SceneToMapConverter(IBehaviorConverterFacade behaviorConverterFacade)
        {
            _behaviorConverterFacade = behaviorConverterFacade;
        }

        public void XXX(GameObject mapGameObject)
        {
            //mapGameObject = new GameObject(); // FIXME: just for testing

            var testyboi = new GameObject();
            testyboi.transform.parent = mapGameObject.transform;
            testyboi.AddComponent<IdentifierBehaviour>();
            testyboi.AddComponent<TypeIdentifierBehaviour>().TypeId = "Actor";
            testyboi.AddComponent<TemplateIdentifierBehaviour>().TemplateId = "Skeleton";
            testyboi.AddComponent<PrefabResourceBehaviour>().Prefab = Resources.Load<GameObject>("mapping/prefabs/actors/actor");
            testyboi.AddComponent<DynamicAnimationBehaviour>();

            foreach (var child in mapGameObject.GetChildGameObjects(true))
            {
                var behaviors = new List<IBehavior>();
                foreach (Component component in child.GetComponents(typeof(Component)))
                {
                    var behavior = _behaviorConverterFacade.Convert(component);
                    if (behavior == null)
                    {
                        continue;
                    }

                    behaviors.Add(behavior);
                }
            }
        }
    }
}
