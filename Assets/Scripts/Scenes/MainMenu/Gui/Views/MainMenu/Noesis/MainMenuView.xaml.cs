#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif


namespace Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu.Noesis
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
            GUI.LoadComponent(this, XamlResolve.ExpectedXamlPath(GetType()));
        }
#endif
    }
}