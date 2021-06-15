#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Noesis
{
    public interface ITitleScreenNoesisViewModel
    {
        ImageSource BackgroundImage { get; }

        ICommand OptionsCommand { get; }

        ICommand ExitCommand { get; }

        ICommand NewGameCommand { get; }
    }
}