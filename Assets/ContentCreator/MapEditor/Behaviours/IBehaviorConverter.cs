using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IBehaviorConverter
    {
        Component Convert(
            GameObject target,
            IBehavior behavior);
        
        IBehavior Convert(Component component);
    }
}