#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using System.Collections.Generic;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.HeaderBar.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis;
using Assets.Scripts.Plugins.Features.InGameMenu;
using Assets.Scripts.Plugins.Features.SceneTransitions;

using Macerus.Plugins.Features.StatusBar.Api;
using Macerus.Plugins.Features.Hud;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;
using ProjectXyz.Shared.Framework;

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
            IStatusBarView statusBarView,
            IHeaderBarView headerBarView,
            IInGameMenuView inGameMenuView,
            IEnumerable<IHudWindow> hudWindows,
            ISceneTransitionView sceneTransitionView,
            IResourceImageSourceFactory resourceImageSourceFactory)
            : this()
        {
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

            var rightContent = NoesisLogicalTreeHelper.FindChildWithName(this, "RightContent");
            var leftContent = NoesisLogicalTreeHelper.FindChildWithName(this, "LeftContent");

            foreach (var hudWindow in hudWindows)
            {
                viewWelderFactory
                .Create<ISimpleWelder>(
                    hudWindow.IsLeftDocked
                        ? leftContent
                        : rightContent,
                    hudWindow)
                .Weld();
            }

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
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "InGameMenuContent"),
                    inGameMenuView)
                .Weld();
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "FaderTransitionContent"),
                    sceneTransitionView)
                .Weld();

            ((Image)NoesisLogicalTreeHelper.FindChildWithName(this, "MinimapCamera")).Source = resourceImageSourceFactory.CreateForResourceId(new StringIdentifier("Minimap/MinimapRenderTexture"));
        }

        public HudView()
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

#if !NOESIS
    internal sealed class TestHudViewModel : IItemDragNoesisViewModel
    {
        public IItemSlotNoesisViewModel DraggedItemSlot => null;
    }
#endif
}
