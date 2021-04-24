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

using Macerus.Plugins.Features.CharacterSheet.Api;

namespace Assets.Scripts.Plugins.Features.CharacterSheet.Noesis.Resources
{
    public partial class CharacterSheetView :
        UserControl,
        ICharacterSheetView
    {
        public CharacterSheetView(ICharacterSheetNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public CharacterSheetView()
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