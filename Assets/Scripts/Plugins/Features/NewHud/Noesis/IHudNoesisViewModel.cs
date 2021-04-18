using Assets.Scripts.Plugins.Features.Inventory.Noesis;

namespace Assets.Scripts.Plugins.Features.NewHud.Noesis
{
    public interface IHudNoesisViewModel
    {
        IItemDragNoesisViewModel ItemDragViewModel { get; }
    }
}