using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
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

        IGameObjectManager GameObjectManager { get; }

        IViewWelderFactory ViewWelderFactory { get; }
    }
}