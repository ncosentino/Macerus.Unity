using Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IHasGuiInventoryBehaviour : IReadonlyHasGuiInventoryBehaviour
    {
        new IItemContainerBehavior ItemContainerBehavior { get; set; }

        new IItemListFactory ItemListFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IGameObjectManager GameObjectManager { get; set; }

        new IViewWelderFactory ViewWelderFactory { get; set; }
    }
}