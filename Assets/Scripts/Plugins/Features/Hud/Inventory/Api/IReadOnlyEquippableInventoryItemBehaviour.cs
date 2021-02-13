using Assets.Scripts.Plugins.Features.Hud.Equipment.Api;

namespace Assets.Scripts.Plugins.Features.Hud.Inventory.Api
{
    public interface IReadOnlyEquippableInventoryItemBehaviour : IReadOnlyEquippableItemBehaviour
    {
        IReadOnlyInventoryItemBehaviour InventoryItemBehaviour { get; }
    }
}