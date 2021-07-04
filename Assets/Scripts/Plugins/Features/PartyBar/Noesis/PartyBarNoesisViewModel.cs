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

using Macerus.Plugins.Features.PartyBar;

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis
{
    public sealed class PartyBarNoesisViewModel :
        MacerusViewModelWrapper,
        IPartyBarNoesisViewModel
    {
        private readonly IPartyBarViewModel _viewModelToWrap;
        private readonly IPartyBarPortraitNoesisViewModelConverter _partyBarPortraitNoesisViewModelConverter;

        private ObservableCollection<IPartyBarPortraitNoesisViewModel> _portraits;


        public PartyBarNoesisViewModel(
            IPartyBarViewModel viewModelToWrap,
            IPartyBarPortraitNoesisViewModelConverter partyBarPortraitNoesisViewModelConverter)
        {
            _portraits = new ObservableCollection<IPartyBarPortraitNoesisViewModel>();
            _viewModelToWrap = viewModelToWrap;
            _partyBarPortraitNoesisViewModelConverter = partyBarPortraitNoesisViewModelConverter;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;

            RefreshPortraits();
        }

        [NotifyForWrappedProperty(nameof(IPartyBarViewModel.Portraits))]
        public IEnumerable<IPartyBarPortraitNoesisViewModel> Portraits => _portraits;

        [NotifyForWrappedProperty(nameof(IPartyBarViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;
        
        private void RefreshPortraits()
        {
            _portraits.Clear();

            foreach (var translated in _viewModelToWrap
               .Portraits
               .Select(_partyBarPortraitNoesisViewModelConverter.Convert))
            {
                _portraits.Add(translated);
            }

            OnPropertyChanged(nameof(Portraits));
        }

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(_viewModelToWrap.Portraits)))
            {
                RefreshPortraits();
                return;
            }

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