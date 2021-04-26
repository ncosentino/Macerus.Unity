#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;
using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.StatusBar.Api;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis.Resources
{
    public partial class StatusBarView :
        UserControl,
        IStatusBarView
    {
        public StatusBarView(IStatusBarNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public StatusBarView()
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