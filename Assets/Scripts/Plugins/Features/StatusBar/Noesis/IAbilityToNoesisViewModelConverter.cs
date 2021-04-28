#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using Macerus.Plugins.Features.StatusBar.Api;

using System;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public interface IAbilityToNoesisViewModelConverter
    {
        Tuple<double, string, ImageSource> Convert(IStatusBarAbilityViewModel viewModel);
    }
}