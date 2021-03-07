#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;

using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif


namespace Assets.Scripts.Gui.Noesis.Views
{
    public partial class Container : ContentControl
    {
        public Container()
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