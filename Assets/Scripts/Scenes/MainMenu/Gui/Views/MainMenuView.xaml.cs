#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif


namespace Assets.Scripts.Scenes.MainMenu.Gui.Views
{
    public partial class MainMenuView : UserControl
    {
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