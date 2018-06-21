using Assets.Scripts.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Wip;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IHasGuiInventoryBehaviour : IReadonlyHasGuiInventoryBehaviour
    {
        new IItemContainer ItemContainer { get; set; }

        new IItemListFactory ItemListFactory { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }

        new IGameObjectManager GameObjectManager { get; set; }
    }
}