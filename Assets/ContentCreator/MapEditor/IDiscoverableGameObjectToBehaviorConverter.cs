
using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IDiscoverableGameObjectToBehaviorConverter : IGameObjectToBehaviorConverter
    {
        bool CanConvert(GameObject unityGameObject);

        bool CanConvert(IBehavior behavior);
    }
}