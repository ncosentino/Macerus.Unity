#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.Noesis
{
    public interface IMainMenuNoesisViewModel
    {
        ImageSource BackgroundImage { get; }

        ICommand CloseCommand { get; }

        ICommand ExitCommand { get; }

        ICommand NewGameCommand { get; }
    }
}