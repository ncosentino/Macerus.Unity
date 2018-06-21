using Assets.Scripts.Gui.Hud.Inventory;
using Assets.Scripts.Unity.GameObjects;
using Assets.Scripts.Wip;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IReadonlyHasGuiInventoryBehaviour
    {
        IItemContainer ItemContainer { get; }

        IItemListFactory ItemListFactory { get; }

        IObjectDestroyer ObjectDestroyer { get; }

        IGameObjectManager GameObjectManager { get; }
    }
}