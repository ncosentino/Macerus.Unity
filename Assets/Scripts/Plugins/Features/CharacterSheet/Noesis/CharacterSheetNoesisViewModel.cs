using Assets.Scripts.Gui.Noesis;

using Noesis;

namespace Assets.Scripts.Plugins.Features.CharacterSheet.Noesis
{
    public sealed class CharacterSheetNoesisViewModel : ICharacterSheetNoesisViewModel
    {
        private readonly ICharacterSheetViewModel _viewModel;
        
        public CharacterSheetNoesisViewModel(ICharacterSheetViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}