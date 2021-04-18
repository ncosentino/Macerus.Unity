#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;

using Assets.Scripts.Gui.Noesis;
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
            IEmptyDropZoneNoesisViewModel emptyDropZoneNoesisViewModel,
            IPlayerInventoryWindow playerInventoryWindow)
        {
            InitializeComponent();
            DataContext = viewModel;

#if NOESIS
            ((FrameworkElement)FindName("EmptySpace")).DataContext = emptyDropZoneNoesisViewModel;

            viewWelderFactory
                .Create<ISimpleWelder>(
                    FindName("RightContent"),
                    playerInventoryWindow)
                .Weld();
#else
            EmptySpace.DataContext = emptyDropZoneNoesisViewModel;

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
