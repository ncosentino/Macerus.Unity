#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using Assets.Scripts.Plugins.Features.Inventory.Noesis;
using Assets.Scripts.Gui.Noesis;

using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.StatusBar.Api;
using Macerus.Plugins.Features.CharacterSheet.Api;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using Assets.Scripts.Plugins.Features.HeaderBar.Noesis;

namespace Assets.Scripts.Plugins.Features.NewHud.Noesis.Resources
{
    public partial class HudView :
        UserControl,
        IHudView
    {
        public HudView(
            IViewWelderFactory viewWelderFactory,
            IItemDragNoesisViewModel viewModel,
            IEmptyDropZoneNoesisViewModel emptyDropZoneNoesisViewModel,
            IPlayerInventoryWindow playerInventoryWindow,
            ICharacterSheetView characterSheetView,
            IStatusBarView statusBarView,
            IHeaderBarView headerBarView)
        {
            InitializeComponent();
            DataContext = viewModel;

            NoesisLogicalTreeHelper
                .FindChildWithName(this, "EmptySpace")
                .DataContext = emptyDropZoneNoesisViewModel;
            NoesisLogicalTreeHelper
                .FindChildWithName(this, "LeftContent")
                .DataContext = emptyDropZoneNoesisViewModel;
            NoesisLogicalTreeHelper
                .FindChildWithName(this, "RightContent")
                .DataContext = emptyDropZoneNoesisViewModel;
            
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "RightContent"),
                    playerInventoryWindow)
                .Weld();

            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "LeftContent"),
                    characterSheetView)
                .Weld();

            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "StatusBarContent"),
                    statusBarView)
                .Weld();
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "HeaderBarContent"),
                    headerBarView)
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
    internal sealed class TestHudViewModel : IItemDragNoesisViewModel
    {
        public IItemSlotNoesisViewModel DraggedItemSlot => null;
    }
#endif
}
