using Assets.Scripts.Plugins.Features.Hud.Inventory.Api;
using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface IReadonlyHasGuiInventoryBehaviour
    {
        IItemContainerBehavior ItemContainerBehavior { get; }

        IItemListFactory ItemListFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IUnityGameObjectManager GameObjectManager { get; }

        IViewWelderFactory ViewWelderFactory { get; }
    }
}