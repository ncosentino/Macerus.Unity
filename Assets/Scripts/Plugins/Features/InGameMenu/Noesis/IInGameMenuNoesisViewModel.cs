#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else

#endif

using System.Windows.Input;

namespace Assets.Scripts.Plugins.Features.InGameMenu.Noesis
{
    public interface IInGameMenuNoesisViewModel
    {
        ICommand CloseCommand { get; }

        ICommand ExitCommand { get; }

        ICommand MainMenuCommand { get; }

        ICommand LoadCommand { get; }

        ICommand SaveCommand { get; }
    }
}