#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;

using Assets.Scripts.Gui.Noesis;

using Macerus.Plugins.Features.MainMenu.Api;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Noesis
{
    public sealed class TitleScreenNoesisViewModel : IMainMenuNoesisViewModel
    {
        private readonly IMainMenuViewModel _viewModelToWrap;

        public TitleScreenNoesisViewModel(
            IMainMenuViewModel viewModelToWrap,
            IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _viewModelToWrap = viewModelToWrap;

            BackgroundImage = resourceImageSourceFactory.CreateForResourceId(viewModelToWrap.BackgroundImageResourceId);
            OptionsCommand = new DelegateCommand(_ => _viewModelToWrap.NavigateOptions());
            ExitCommand = new DelegateCommand(_ => _viewModelToWrap.ExitGame());
            NewGameCommand = new DelegateCommand(_ => _viewModelToWrap.StartNewGame());
        }

        public ImageSource BackgroundImage { get; }

        public ICommand OptionsCommand { get; }

        public ICommand ExitCommand { get; }

        public ICommand NewGameCommand { get; }
    }
}