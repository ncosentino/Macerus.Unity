using System;
using System.Collections.Generic;

using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EditorNameBehavior : BaseBehavior
    {
        public EditorNameBehavior(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public sealed class EditorNameBehaviorConverter : IDiscoverableBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is EditorNameBehavior;

        public bool CanConvert(Component component) => false;

        public Component Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (EditorNameBehavior)behavior;
            target.name = castedBehavior.Name;
            return null;
        }

        public IBehavior Convert(Component component) =>
            throw new NotSupportedException();
    }

    public sealed class GameObjectConverter : IGameObjectConverter
    {
        private IBehaviorConverterFacade _behaviorConverterFacade;
        private IGameObjectToBehaviorConverterFacade _gameObjectToBehaviorConverterFacade;

        public GameObjectConverter(
            Lazy<IBehaviorConverterFacade> behaviorConverterFacade,
            Lazy<IGameObjectToBehaviorConverterFacade> gameObjectToBehaviorConverterFacade)
        {
            _behaviorConverterFacade = behaviorConverterFacade.Value;
            _gameObjectToBehaviorConverterFacade = gameObjectToBehaviorConverterFacade.Value;
        }

        public GameObject Convert(IEnumerable<IBehavior> behaviors)
        {
            var unityGameObject = new GameObject();

            foreach (var behavior in behaviors)
            {
                _behaviorConverterFacade.Convert(
                    unityGameObject,
                    behavior);

                var childGameObject = _gameObjectToBehaviorConverterFacade.Convert(behavior);
                if (childGameObject != null)
                {
                    childGameObject.transform.parent = unityGameObject.transform;
                }
            }

            return unityGameObject;
        }

        public IEnumerable<IBehavior> Convert(GameObject unityGameObject)
        {
            yield return new EditorNameBehavior(unityGameObject.name);

            foreach (var component in unityGameObject.GetComponents(typeof(Component)))
            {
                var behavior = _behaviorConverterFacade.Convert(component);
                if (behavior == null)
                {
                    continue;
                }

                yield return behavior;
            }

            // handle nested objects to handle hierarchical information for behaviors
            foreach (var childUnityObject in unityGameObject.GetChildGameObjects(true))
            {
                //bool editorOnly = childUnityObject.name.StartsWith(
                //    "_",
                //    StringComparison.Ordinal);
                //if (editorOnly)
                //{
                //    continue;
                //}

                var behavior = _gameObjectToBehaviorConverterFacade.Convert(childUnityObject);
                if (behavior == null)
                {
                    continue;
                }

                yield return behavior;
            }
        }
    }
}