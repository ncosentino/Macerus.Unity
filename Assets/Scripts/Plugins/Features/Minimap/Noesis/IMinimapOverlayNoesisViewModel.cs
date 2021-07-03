#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Media;
#endif

using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.Minimap.Noesis
{
    public interface IMinimapOverlayNoesisViewModel : ITransparentToGameInteraction
    {
        Visibility Visibility { get; }

        ImageSource CameraSource { get; }
    }
}