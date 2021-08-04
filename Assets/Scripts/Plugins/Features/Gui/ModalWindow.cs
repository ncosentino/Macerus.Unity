#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace Assets.Scripts.Plugins.Features.Gui
{
    public sealed class ModalWindow : 
        Grid,
        IModalWindow
    {
        public ModalWindow()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            Background = new SolidColorBrush(Color.FromArgb(0xC0, 0, 0, 0));
            Visibility = Visibility.Collapsed;
        }
    }
}
