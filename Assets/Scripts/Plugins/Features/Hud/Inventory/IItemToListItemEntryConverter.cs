using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory
{
    public interface IItemToListItemEntryConverter
    {
        IInventoryListItemPrefab Convert(
            IGameObject item,
            string itemListEntryPrefabResource);
    }
}