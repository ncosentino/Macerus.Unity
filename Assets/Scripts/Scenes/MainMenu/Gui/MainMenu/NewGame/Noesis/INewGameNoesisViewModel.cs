#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System.Windows.Input;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.NewGame.Noesis
{
    public interface INewGameNoesisViewModel
    {
        ICommand NewGameCommand { get; }

        ICommand BackCommand { get; }
    }
}