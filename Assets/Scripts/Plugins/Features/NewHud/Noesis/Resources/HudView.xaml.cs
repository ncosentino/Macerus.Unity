#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

using Assets.Scripts.Plugins.Features.Inventory.Noesis;
using Macerus.Plugins.Features.Inventory.Api;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Plugins.Features.NewHud.Noesis.Resources
{
    public partial class HudView :
        UserControl
    {
        public HudView(
            IViewWelderFactory viewWelderFactory,
            IItemDragNoesisViewModel viewModel,
            IPlayerInventoryWindow playerInventoryWindow)
        {
            InitializeComponent();
            DataContext = viewModel;

#if NOESIS
            // weld
#else
            viewWelderFactory
                .Create<ISimpleWelder>(
                    RightContent,
                    playerInventoryWindow)
                .Weld();
#endif
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif
    }

#if !NOESIS
    internal sealed class TestHudViewModel : IItemDragNoesisViewModel
    {
        public IItemSlotNoesisViewModel DraggedItemSlot => null;
    }
#endif
}
