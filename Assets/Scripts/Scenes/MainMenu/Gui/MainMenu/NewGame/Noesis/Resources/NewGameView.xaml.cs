#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System.Windows.Controls;
#endif

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.NewGame.Noesis.Resources
{
    public partial class NewGameView :
        UserControl,
        INewGameView
    {
        public NewGameView(INewGameNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public NewGameView()
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