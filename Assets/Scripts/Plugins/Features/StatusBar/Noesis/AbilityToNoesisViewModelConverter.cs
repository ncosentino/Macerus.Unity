#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
using Color = Noesis.Color;
#else
using System.Windows.Media;
#endif

using System;

using Assets.Scripts.Gui.Noesis;
using Macerus.Plugins.Features.StatusBar.Api;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public sealed class AbilityToNoesisViewModelConverter : IAbilityToNoesisViewModelConverter
    {
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;

        public AbilityToNoesisViewModelConverter(IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _resourceImageSourceFactory = resourceImageSourceFactory;
        }

        public Tuple<double, string, ImageSource> Convert(IStatusBarAbilityViewModel viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }

            return Tuple.Create(
                viewModel.IsEnabled ? 1.0d : 0.3d,
                viewModel.AbilityName,
                _resourceImageSourceFactory.CreateForResourceId(viewModel.IconResourceId)); ;
        }
    }
}