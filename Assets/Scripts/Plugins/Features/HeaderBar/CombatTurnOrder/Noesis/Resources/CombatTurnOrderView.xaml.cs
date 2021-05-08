#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;
using Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis.Resources
{
    public partial class CombatTurnOrderView :
        UserControl,
        ICombatTurnOrderView
    {
        public CombatTurnOrderView(ICombatTurnOrderNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public CombatTurnOrderView()
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