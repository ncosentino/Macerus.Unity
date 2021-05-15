using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IGameObjectConverter
    {
        GameObject Convert(IEnumerable<IBehavior> behaviors);

        IEnumerable<IBehavior> Convert(GameObject unityGameObject);
    }
}