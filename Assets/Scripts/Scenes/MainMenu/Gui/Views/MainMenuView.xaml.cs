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


namespace Assets.Scripts.Scenes.MainMenu.Gui.Views
{
    public partial class MainMenuView :
        UserControl,
        IMainMenuView
    {
        public MainMenuView(IMainMenuViewModel viewModel)
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
            Noesis.GUI.LoadComponent(this, XamlResolve.ExpectedXamlPath(GetType()));
        }
#endif
    }
}