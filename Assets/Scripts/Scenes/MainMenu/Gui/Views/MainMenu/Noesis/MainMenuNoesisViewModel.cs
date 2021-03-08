using System.Windows.Input;

using Assets.Scripts.Gui.Noesis;

using Noesis;

namespace Assets.Scripts.Scenes.MainMenu.Gui.Views.MainMenu.Noesis
{
    public sealed class MainMenuNoesisViewModel : IMainMenuNoesisViewModel
    {
        private readonly IMainMenuViewModel _viewModel;
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;

        public MainMenuNoesisViewModel(
            IMainMenuViewModel viewModel,
            IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _viewModel = viewModel;
            _resourceImageSourceFactory = resourceImageSourceFactory;
            BackgroundImage = _resourceImageSourceFactory.CreateForResourceId(_viewModel.BackgroundImageResourceId);
            ExitCommand = new DelegateCommand(_ => _viewModel.RequestExit());
            CloseCommand = new DelegateCommand(_ => _viewModel.RequestClose());
            NewGameCommand = new DelegateCommand(_ => _viewModel.RequestNewGame());
        }

        public ImageSource BackgroundImage { get; }

        public ICommand ExitCommand { get; }

        public ICommand CloseCommand { get; }

        public ICommand NewGameCommand { get; }
    }
}