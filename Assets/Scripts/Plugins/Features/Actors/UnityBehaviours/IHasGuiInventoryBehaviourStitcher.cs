using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IHasGuiInventoryBehaviourStitcher
    {
        IReadonlyHasGuiInventoryBehaviour Attach(
            GameObject gameObject,
            IItemContainerBehavior itemContainerBehavior);
    }
}