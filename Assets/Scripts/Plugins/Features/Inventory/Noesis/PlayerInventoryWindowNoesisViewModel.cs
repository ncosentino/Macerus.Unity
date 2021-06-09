#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Gui.Default;

using Assets.Scripts.Gui.Noesis.ViewModels;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class PlayerInventoryWindowNoesisViewModel :
        NotifierBase,
        IPlayerInventoryWindowNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IPlayerInventoryViewModel _viewModelToWrap;

        static PlayerInventoryWindowNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<PlayerInventoryWindowNoesisViewModel>();
        }

        public PlayerInventoryWindowNoesisViewModel(IPlayerInventoryViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        [NotifyForWrappedProperty(nameof(IPlayerInventoryViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        public bool IsLeftDocked => _viewModelToWrap.IsLeftDocked;

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