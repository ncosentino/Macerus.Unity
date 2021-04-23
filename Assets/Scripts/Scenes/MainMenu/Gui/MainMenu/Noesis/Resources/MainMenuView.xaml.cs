#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.Noesis.Resources
{
    public partial class MainMenuView :
        UserControl,
        IMainMenuView
    {
        public MainMenuView(IMainMenuNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
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