using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IBehaviorToBaseGameObjectConverter
    {
        GameObject Convert(IBehavior behavior);
    }
}