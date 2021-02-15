
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IHasGuiInventoryBehaviourStitcher
    {
        IReadonlyHasGuiInventoryBehaviour Attach(
            GameObject gameObject,
            IItemContainerBehavior itemContainerBehavior);
    }
}