#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.CharacterSheet.Api;
using System.Collections.ObjectModel;

namespace Assets.Scripts.Plugins.Features.CharacterSheet.Noesis
{
    public sealed class CharacterSheetNoesisViewModel :
        NotifierBase,
        ICharacterSheetNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly ICharacterSheetViewModel _viewModelToWrap;
        private readonly ObservableCollection<Tuple<string, string>> _translatedStatViewModels;

        static CharacterSheetNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<CharacterSheetNoesisViewModel>();
        }

        public CharacterSheetNoesisViewModel(ICharacterSheetViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;

            _translatedStatViewModels = new ObservableCollection<Tuple<string, string>>();
        }

        [NotifyForWrappedProperty(nameof(ICharacterSheetViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        [NotifyForWrappedProperty(nameof(ICharacterSheetViewModel.Stats))]
        public IEnumerable<Tuple<string, string>> Stats => _translatedStatViewModels;

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(_viewModelToWrap.Stats)))
            {
                _translatedStatViewModels.Clear();
                foreach (var translated in _viewModelToWrap
                   .Stats
                   .Select(x => Tuple.Create(x.Name, x.DisplayValue)))
                {
                    _translatedStatViewModels.Add(translated);
                }
            }

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