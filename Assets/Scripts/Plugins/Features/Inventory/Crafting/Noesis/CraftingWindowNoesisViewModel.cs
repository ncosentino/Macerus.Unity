#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using Macerus.Plugins.Features.Inventory.Api.Crafting;
using Macerus.Plugins.Features.Gui.Default;

using Assets.Scripts.Gui.Noesis.ViewModels;
using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Crafting.Noesis;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class CraftingWindowNoesisViewModel :
        NotifierBase,
        ICraftingWindowNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly ICraftingWindowViewModel _viewModelToWrap;

        static CraftingWindowNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<CraftingWindowNoesisViewModel>();
        }

        public CraftingWindowNoesisViewModel(ICraftingWindowViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
            CraftCommand = new DelegateCommand(_ => _viewModelToWrap.Craft());
        }

        [NotifyForWrappedProperty(nameof(ICraftingWindowViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        public ICommand CraftCommand { get; }

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