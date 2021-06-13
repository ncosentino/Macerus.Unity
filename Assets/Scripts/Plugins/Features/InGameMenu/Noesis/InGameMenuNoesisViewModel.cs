#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else

#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.InGameMenu.Api;

namespace Assets.Scripts.Plugins.Features.InGameMenu.Noesis
{
    public sealed class InGameMenuNoesisViewModel :
        NotifierBase,
        IInGameMenuNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IInGameMenuViewModel _viewModelToWrap;
        
        static InGameMenuNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<InGameMenuNoesisViewModel>();
        }

        public InGameMenuNoesisViewModel(IInGameMenuViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;

            CloseCommand = new DelegateCommand(_ => _viewModelToWrap.Close());
            ExitCommand = new DelegateCommand(_ => _viewModelToWrap.ExitGame());
            MainMenuCommand = new DelegateCommand(_ => _viewModelToWrap.GoToMainMenu());
        }

        [NotifyForWrappedProperty(nameof(IInGameMenuViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        public ICommand CloseCommand { get; }

        public ICommand ExitCommand { get; }

        public ICommand MainMenuCommand { get; }

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (!_lazyNotifierMapping.Value.TryGetValue(
                e.PropertyName,
                out var propertyName))
            {
                return;
            }

            OnPropertyChanged(propertyName);
        }
    }
}