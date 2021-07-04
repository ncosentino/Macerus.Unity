#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using Color = Noesis.Color;
#else
using System.Windows.Media;
#endif

using Assets.Scripts.Gui.Noesis;

using Macerus.Plugins.Features.PartyBar;

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis
{
    public sealed class PartyBarPortraitNoesisViewModelConverter : IPartyBarPortraitNoesisViewModelConverter
    {
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;

        public PartyBarPortraitNoesisViewModelConverter(IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _resourceImageSourceFactory = resourceImageSourceFactory;
        }

        public IPartyBarPortraitNoesisViewModel Convert(IPartyBarPortraitViewModel viewModel)
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
            var convertedViewModel = new PartyBarPortraitNoesisViewModel(
                imageSource,
                viewModel.ActorIdentifier,
                borderBrush,
                backgroundBrush,
                viewModel.ActorName,
                viewModel.Activate);
            return convertedViewModel;
        }
    }
}