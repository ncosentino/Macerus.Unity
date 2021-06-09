#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Gui.Noesis;
using Noesis;
#else
using System.Windows.Controls;
#endif

using Macerus.Plugins.Features.CharacterSheet.Api;

namespace Assets.Scripts.Plugins.Features.CharacterSheet.Noesis.Resources
{
    public partial class CharacterSheetView :
        UserControl,
        ICharacterSheetView
    {
        private readonly ICharacterSheetNoesisViewModel _viewModel;

        public CharacterSheetView(ICharacterSheetNoesisViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
            _viewModel = viewModel;
        }

        public CharacterSheetView()
        {
            InitializeComponent();
        }

        public bool IsLeftDocked => _viewModel.IsLeftDocked;

#if NOESIS
        private void InitializeComponent()
        {
            NoesisComponentInitializer.InitializeComponentXaml(this);
        }
#endif
    }
}