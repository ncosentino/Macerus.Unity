#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Plugins.Features.HeaderBar.Noesis.Resources
{
    public partial class HeaderBarView :
        UserControl,
        IHeaderBarView
    {
        public HeaderBarView(
            IHeaderBarNoesisViewModel viewModel,
            IViewWelderFactory viewWelderFactory,
            ICombatTurnOrderView combatTurnOrderView)
            : this()
        {
            DataContext = viewModel;

            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "CombatTurnOrderContent"),
                    combatTurnOrderView)
                .Weld();
        }

        public HeaderBarView()
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