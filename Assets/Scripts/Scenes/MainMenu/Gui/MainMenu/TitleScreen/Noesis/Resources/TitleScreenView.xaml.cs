#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Noesis.Resources
{
    public partial class TitleScreenView :
        UserControl,
        ITitleScreenView
    {
        public TitleScreenView(IMainMenuNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public TitleScreenView()
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