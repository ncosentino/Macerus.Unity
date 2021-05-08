#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis
{
    public sealed class CombatTurnOrderNoesisViewModel :
        NotifierBase,
        ICombatTurnOrderNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly ICombatTurnOrderViewModel _viewModelToWrap;
        private readonly ICombatPortraitNoesisViewModelConverter _combatPortraitNoesisViewModelConverter;

        private ObservableCollection<ICombatTurnOrderPortraitNoesisViewModel> _portraits;

        static CombatTurnOrderNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<CombatTurnOrderNoesisViewModel>();
        }

        public CombatTurnOrderNoesisViewModel(
            ICombatTurnOrderViewModel viewModelToWrap,
            ICombatPortraitNoesisViewModelConverter combatPortraitNoesisViewModelConverter)
        {
            _portraits = new ObservableCollection<ICombatTurnOrderPortraitNoesisViewModel>();
            _viewModelToWrap = viewModelToWrap;
            _combatPortraitNoesisViewModelConverter = combatPortraitNoesisViewModelConverter;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        [NotifyForWrappedProperty(nameof(ICombatTurnOrderViewModel.Portraits))]
        public IEnumerable<ICombatTurnOrderPortraitNoesisViewModel> Portraits => _portraits;

        [NotifyForWrappedProperty(nameof(ICombatTurnOrderViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(_viewModelToWrap.Portraits)))
            {
                _portraits.Clear();

                foreach (var translated in _viewModelToWrap
                   .Portraits
                   .Select(_combatPortraitNoesisViewModelConverter.Convert))
                {
                    _portraits.Add(translated);
                }

                OnPropertyChanged(nameof(Portraits));

                return;
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