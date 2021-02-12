using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IItemToListItemEntryConverter
    {
        IInventoryListItemPrefab Convert(
            IGameObject item,
            string itemListEntryPrefabResource);
    }
}