#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;
using Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis.Resources
{
    public partial class PartyBarView :
        UserControl,
        IPartyBarView
    {
        public PartyBarView(IPartyBarNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public PartyBarView()
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