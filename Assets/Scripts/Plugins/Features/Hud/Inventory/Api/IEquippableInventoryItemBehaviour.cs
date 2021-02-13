namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IEquippableInventoryItemBehaviour : IReadOnlyEquippableInventoryItemBehaviour
    {
        new IReadOnlyInventoryItemBehaviour InventoryItemBehaviour { get; set; }
    }
}