#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Controls;
#endif

using Assets.Scripts.Gui.Noesis;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using Macerus.Plugins.Features.Inventory.Api.Crafting;
using Macerus.Plugins.Features.Gui.Default;

namespace Assets.Scripts.Plugins.Features.Inventory.Crafting.Noesis.Resources
{
    public partial class CraftingWindow :
        UserControl,
        ICraftingWindow
    {
        private readonly ICraftingWindowNoesisViewModel _viewModel;

        public CraftingWindow(
            IViewWelderFactory viewWelderFactory,
            ICraftingWindowNoesisViewModel viewModel,
            ICraftingBagView craftingBagView)
            : this()
        {
            DataContext = viewModel;

            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "BagPlaceholder"),
                    craftingBagView)
                .Weld();
            _viewModel = viewModel;
        }

        public CraftingWindow()
        {
            InitializeComponent();
        }

        public bool IsLeftDocked => _viewModel.IsLeftDocked;

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif
    }

#if !NOESIS
    internal sealed class TestCraftingWindow :
        NotifierBase,
        ICraftingWindowNoesisViewModel
    {
        public Visibility Visibility => Visibility.Visible;

        public bool IsLeftDocked { get; } = true;
    }
#endif
}
