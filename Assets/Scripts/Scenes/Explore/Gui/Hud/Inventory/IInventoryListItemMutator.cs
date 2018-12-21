using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IInventoryListItemMutator
    {
        void Mutate(
            IInventoryListItemPrefab inventoryListItemPrefab,
            IGameObject item);
    }
}