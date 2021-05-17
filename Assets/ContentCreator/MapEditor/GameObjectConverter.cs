using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class GameObjectConverter : IGameObjectConverter
    {
        private readonly IBehaviorToBaseGameObjectConverterFacade _behaviorToBaseGameObjectConverterFacade;
        private readonly IBehaviorConverterFacade _behaviorConverterFacade;
        private readonly IGameObjectToBehaviorConverterFacade _gameObjectToBehaviorConverterFacade;

        public GameObjectConverter(
            Lazy<IBehaviorConverterFacade> behaviorConverterFacade,
            Lazy<IGameObjectToBehaviorConverterFacade> gameObjectToBehaviorConverterFacade,
            Lazy<IBehaviorToBaseGameObjectConverterFacade> behaviorToBaseGameObjectConverterFacade)
        {
            _behaviorConverterFacade = behaviorConverterFacade.Value;
            _gameObjectToBehaviorConverterFacade = gameObjectToBehaviorConverterFacade.Value;
            _behaviorToBaseGameObjectConverterFacade = behaviorToBaseGameObjectConverterFacade.Value;
        }

        public GameObject Convert(IEnumerable<IBehavior> behaviors)
        {
            var unityGameObject = behaviors
                .Select(x => _behaviorToBaseGameObjectConverterFacade.Convert(x))
                .FirstOrDefault(x => x != null)
                ?? new GameObject();

            foreach (var behavior in behaviors)
            {
                _behaviorConverterFacade
                    .Convert(
                        unityGameObject,
                        behavior)
                    .Consume();

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
            foreach (var behavior in _gameObjectToBehaviorConverterFacade.Convert(unityGameObject))
            {
                yield return behavior;
            }

            foreach (var component in unityGameObject.GetComponents(typeof(Component)))
            {
                foreach (var behavior in _behaviorConverterFacade.Convert(component))
                {
                    yield return behavior;
                }                
            }

            // handle nested objects to handle hierarchical information for behaviors
            foreach (var childUnityObject in unityGameObject.GetChildGameObjects(true))
            {
                bool editorOnly = childUnityObject.name.StartsWith(
                    "_",
                    StringComparison.Ordinal);
                if (editorOnly)
                {
                    continue;
                }

                foreach (var behavior in Convert(childUnityObject))
                {
                    yield return behavior;
                }
            }
        }
    }
}