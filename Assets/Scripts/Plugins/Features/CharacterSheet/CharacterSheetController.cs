using System;

using UnityEngine.SceneManagement;

namespace Assets.Scripts.Plugins.Features.CharacterSheet
{
    public sealed class CharacterSheetController
    {
        private ICharacterSheetViewModel _characterSheetViewModel;

        public CharacterSheetController(ICharacterSheetViewModel characterSheetViewModel)
        {
            _characterSheetViewModel = characterSheetViewModel;
        }
    }
}