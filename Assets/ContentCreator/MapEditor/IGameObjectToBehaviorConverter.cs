using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IGameObjectToBehaviorConverter
    {
        GameObject Convert(IBehavior behavior);

        IEnumerable<IBehavior> Convert(GameObject unityGameObject);
    }
}