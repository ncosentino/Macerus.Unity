#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;

using Assets.Scripts.Gui.Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Plugins.Features.Minimap.Noesis.Resources
{
    public partial class MinimapOverlayView :
        UserControl,
        IMinimapOverlayView
    {
        public MinimapOverlayView(IMinimapOverlayNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public MinimapOverlayView()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif
    }
}