#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Plugins.Features.InGameMenu.Noesis.Resources
{
    public partial class InGameMenuView :
        UserControl,
        IInGameMenuView
    {
        public InGameMenuView(IInGameMenuNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public InGameMenuView()
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