#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.Gui.Api.SceneTransitions;

namespace Assets.Scripts.Plugins.Features.SceneTransitions.Noesis.Resources
{
    public partial class FaderSceneTransitionView :
        UserControl,
        ISceneTransitionView
    {
        public FaderSceneTransitionView(IFaderSceneTransitionViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public FaderSceneTransitionView()
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