using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Shared.Framework;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class ImplicitIdentifierBehaviorConverter :
        IDiscoverableGameObjectToBehaviorConverter
    {
        public bool CanConvert(GameObject unityGameObject) =>
            // only add one if we don't have one that will explicitly get added
            unityGameObject.GetComponent<IdentifierBehaviour>() == null;

        public bool CanConvert(IBehavior behavior) => false;

        public GameObject Convert(IBehavior behavior) =>
            throw new NotSupportedException();

        public IEnumerable<IBehavior> Convert(GameObject unityGameObject)
        {
            yield return new IdentifierBehavior(new StringIdentifier(Guid.NewGuid().ToString()));
        }
    }
}
