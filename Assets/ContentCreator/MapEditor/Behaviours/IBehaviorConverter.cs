using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public interface IBehaviorConverter
    {
        IEnumerable<Component> Convert(
            GameObject target,
            IBehavior behavior);

        IEnumerable<IBehavior> Convert(Component component);
    }
}