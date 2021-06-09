using Assets.Scripts.Plugins.Features.Inventory.Noesis;

using Macerus.Plugins.Features.Hud;

namespace Assets.Scripts.Plugins.Features.NewHud.Noesis
{
    public sealed class HudNoesisViewModel : IHudNoesisViewModel
    {
        private readonly IHudViewModel _viewModel;

        public HudNoesisViewModel(
            IHudViewModel viewModel,
            IItemDragNoesisViewModel itemDragViewModel)
        {
            _viewModel = viewModel;
            ItemDragViewModel = itemDragViewModel;
        }

        public IItemDragNoesisViewModel ItemDragViewModel { get; }
    }
}
