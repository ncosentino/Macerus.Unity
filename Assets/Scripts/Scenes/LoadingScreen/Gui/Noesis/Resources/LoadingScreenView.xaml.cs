#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Controls;
#endif

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.SceneTransitions;

using ProjectXyz.Framework.ViewWelding.Api;
using ProjectXyz.Framework.ViewWelding.Api.Welders;

namespace Assets.Scripts.Scenes.LoadingScreen.Gui.Noesis.Resources
{
    public partial class LoadingScreenView :
        UserControl,
        ILoadingScreenView
    {
        public LoadingScreenView(
            IViewWelderFactory viewWelderFactory,
            ISceneTransitionView sceneTransitionView)
            : this()
        {
            viewWelderFactory
                .Create<ISimpleWelder>(
                    NoesisLogicalTreeHelper.FindChildWithName(this, "TransitionViewContent"),
                    sceneTransitionView)
                .Weld();
        }

        public LoadingScreenView()
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