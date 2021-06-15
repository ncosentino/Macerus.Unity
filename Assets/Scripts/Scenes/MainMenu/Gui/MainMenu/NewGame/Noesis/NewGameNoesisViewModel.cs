#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.MainMenu.Api.NewGame;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.NewGame.Noesis
{
    public sealed class NewGameNoesisViewModel :
        MacerusViewModelWrapper,
        INewGameNoesisViewModel
    {
        private readonly INewGameViewModel _viewModelToWrap;

        public NewGameNoesisViewModel(INewGameViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;

            BackCommand = new DelegateCommand(_ => _viewModelToWrap.GoBack());
            NewGameCommand = new DelegateCommand(_ => _viewModelToWrap.StartNewGame());

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        public ICommand BackCommand { get; }

        public ICommand NewGameCommand { get; }

        [NotifyForWrappedProperty(nameof(INewGameViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (!NotifierMapping.TryGetValue(
                e.PropertyName,
                out var propertyName))
            {
                return;
            }

            OnPropertyChanged(propertyName);
        }
    }
}