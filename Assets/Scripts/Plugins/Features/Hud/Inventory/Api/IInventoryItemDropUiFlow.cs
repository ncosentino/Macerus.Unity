namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IInventoryItemDropUiFlow
    {
        void Execute(DroppedEventArgs droppedEventArgs);
    }
}