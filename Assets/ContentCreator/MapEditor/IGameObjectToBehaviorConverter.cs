
using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IGameObjectToBehaviorConverter
    {
        GameObject Convert(IBehavior behavior);

        IBehavior Convert(GameObject unityGameObject);
    }
}