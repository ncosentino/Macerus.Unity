using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IDiscoverableBehaviorConverter : IBehaviorConverter
    {
        bool CanConvert(IBehavior behavior);

        bool CanConvert(Component component);
    }
}