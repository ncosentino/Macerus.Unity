using System;
using System.Collections.Generic;

using Macerus.ContentCreator.MapEditor.Behaviors.Shared;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class EditorNameBehaviorConverter : 
        IDiscoverableBehaviorConverter,
        IDiscoverableGameObjectToBehaviorConverter
    {
        public bool CanConvert(IBehavior behavior) => behavior is EditorNameBehavior;

        public bool CanConvert(Component component) => false;

        public bool CanConvert(GameObject unityGameObject) => true;

        public IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior)
        {
            var castedBehavior = (EditorNameBehavior)behavior;
            target.name = castedBehavior.Name;
            yield break;
        }

        public IEnumerable<IBehavior> Convert(Component component) =>
            throw new NotSupportedException();

        public GameObject Convert(IBehavior behavior) => null;

        public IEnumerable<IBehavior> Convert(GameObject unityGameObject)
        {
            var behavior = new EditorNameBehavior(unityGameObject.name);
            yield return behavior;
        }
    }
}