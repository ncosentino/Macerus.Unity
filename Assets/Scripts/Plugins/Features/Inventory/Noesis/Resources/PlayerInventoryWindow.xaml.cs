#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.Inventory.Api;
using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis.Resources
{
    public partial class PlayerInventoryWindow :
        UserControl,
        IPlayerInventoryWindow
    {
        public PlayerInventoryWindow(
            IViewWelderFactory viewWelderFactory,
            IItemDragNoesisViewModel viewModel,
            IInventoryEquipmentView inventoryEquipmentView,
            IInventoryBagView inventoryBagView)
        {
            InitializeComponent();
            DataContext = viewModel;

#if NOESIS
            viewWelderFactory
                .Create<ISimpleWelder>(
                    FindName("EquipmentPlaceholder"),
                    inventoryEquipmentView)
                .Weld();
            viewWelderFactory
                .Create<ISimpleWelder>(
                    FindName("BagPlaceholder"),
                    inventoryBagView)
                .Weld();
#else
            viewWelderFactory
                .Create<ISimpleWelder>(
                    EquipmentPlaceholder,
                    inventoryEquipmentView)
                .Weld();
            viewWelderFactory
                .Create<ISimpleWelder>(
                    BagPlaceholder,
                    inventoryBagView)
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
    internal sealed class TestItemDragNoesisViewModel : IItemDragNoesisViewModel
    {
        public IItemSlotNoesisViewModel DraggedItemSlot => null;
    }
#endif
}
