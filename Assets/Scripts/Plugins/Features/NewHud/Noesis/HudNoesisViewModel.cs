using Assets.Scripts.Plugins.Features.Inventory.Noesis;

namespace Assets.Scripts.Plugins.Features.NewHud.Noesis
{
    public sealed class HudNoesisViewModel : IHudNoesisViewModel
    {
        public HudNoesisViewModel(IItemDragNoesisViewModel itemDragViewModel)
        {
            ItemDragViewModel = itemDragViewModel;
        }

        public IItemDragNoesisViewModel ItemDragViewModel { get; }
    }
}
