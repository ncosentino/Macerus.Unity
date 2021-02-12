using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IInventoryListItemMutator
    {
        void Mutate(
            IInventoryListItemPrefab inventoryListItemPrefab,
            IGameObject item);
    }
}