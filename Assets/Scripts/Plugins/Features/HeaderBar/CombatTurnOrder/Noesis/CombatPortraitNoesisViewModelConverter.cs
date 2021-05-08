#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using Color = Noesis.Color;
#else
using System.Windows.Media;
#endif

using Assets.Scripts.Gui.Noesis;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis
{
    public sealed class CombatPortraitNoesisViewModelConverter : ICombatPortraitNoesisViewModelConverter
    {
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;

        public CombatPortraitNoesisViewModelConverter(IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _resourceImageSourceFactory = resourceImageSourceFactory;
        }

        public ICombatTurnOrderPortraitNoesisViewModel Convert(ICombatTurnOrderPortraitViewModel viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }

            var imageSource = _resourceImageSourceFactory.CreateForResourceId(viewModel.IconResourceId);
            var borderBrush = new SolidColorBrush(Color.FromArgb(
                (byte)viewModel.BorderColor.A,
                (byte)viewModel.BorderColor.R,
                (byte)viewModel.BorderColor.G, 
                (byte)viewModel.BorderColor.B));
            var backgroundBrush = new SolidColorBrush(Color.FromArgb(
                (byte)viewModel.BackgroundColor.A,
                (byte)viewModel.BackgroundColor.R,
                (byte)viewModel.BackgroundColor.G,
                (byte)viewModel.BackgroundColor.B));
            var convertedViewModel = new CombatTurnOrderPortraitNoesisViewModel(
                imageSource,
                viewModel.ActorIdentifier,
                borderBrush,
                backgroundBrush,
                viewModel.ActorName);
            return convertedViewModel;
        }
    }
}