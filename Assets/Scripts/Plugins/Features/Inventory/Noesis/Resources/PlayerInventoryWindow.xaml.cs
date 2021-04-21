#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Controls;
#endif

using Assets.Scripts.Gui.Noesis;

using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Gui.Default;

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
            IPlayerInventoryWindowNoesisViewModel viewModel,
            IInventoryEquipmentView inventoryEquipmentView,
            IInventoryBagView inventoryBagView)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "EquipmentPlaceholder"),
                    inventoryEquipmentView)
                .Weld();
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "BagPlaceholder"),
                    inventoryBagView)
                .Weld();
        }

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif
    }

#if !NOESIS
    internal sealed class TestPlayerInventoryWindow :
        NotifierBase,
        IPlayerInventoryWindowNoesisViewModel
    {
        public Visibility Visibility => Visibility.Visible;
    }
#endif
}
