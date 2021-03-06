﻿#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;
using System.ComponentModel;
using System.Windows;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.MainMenu.Api;

namespace Assets.Scripts.Scenes.MainMenu.Gui.MainMenu.TitleScreen.Noesis
{
    public sealed class TitleScreenNoesisViewModel :
        MacerusViewModelWrapper,
        ITitleScreenNoesisViewModel
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

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        public ImageSource BackgroundImage { get; }

        public ICommand OptionsCommand { get; }

        public ICommand ExitCommand { get; }

        public ICommand NewGameCommand { get; }

        [NotifyForWrappedProperty(nameof(IMainMenuViewModel.IsOpen))]
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