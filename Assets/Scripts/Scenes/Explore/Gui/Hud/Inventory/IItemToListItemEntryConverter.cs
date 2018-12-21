using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Scenes.Explore.Gui.Hud.Inventory
{
    public interface IItemToListItemEntryConverter
    {
        IInventoryListItemPrefab Convert(
            IGameObject item,
            string itemListEntryPrefabResource);
    }
}