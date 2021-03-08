using System.Windows.Input;

using Noesis;

namespace Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu.Noesis
{
    public interface IMainMenuNoesisViewModel
    {
        ImageSource BackgroundImage { get; }

        ICommand CloseCommand { get; }

        ICommand ExitCommand { get; }

        ICommand NewGameCommand { get; }
    }
}