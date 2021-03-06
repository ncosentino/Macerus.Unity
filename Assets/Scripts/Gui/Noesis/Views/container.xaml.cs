#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif


namespace Assets.Scripts.Gui.Noesis.Views
{
    public partial class Container : Viewbox
    {
        public Container()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/Gui/Noesis/Views/Container.xaml");
        }
#endif
    }
}