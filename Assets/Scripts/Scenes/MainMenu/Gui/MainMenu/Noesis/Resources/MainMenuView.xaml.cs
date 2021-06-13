#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.SceneTransitions;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.Noesis.Resources
{
    public partial class MainMenuView :
        UserControl,
        IMainMenuView
    {
        public MainMenuView(
            IViewWelderFactory viewWelderFactory,
            ISceneTransitionView sceneTransitionView,
            ITitleScreenView titleScreenView)
            : this()
        {
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "MainContent"),
                    titleScreenView)
                .Weld();
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "TransitionViewContent"),
                    sceneTransitionView)
                .Weld();
        }

        public MainMenuView()
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