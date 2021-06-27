#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.Concurrent;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.StatusBar.Api;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public sealed class StatusBarNoesisViewModel :
        NotifierBase,
        IStatusBarNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IUiDispatcher _uiDispatcher;
        private readonly IStatusBarViewModel _viewModelToWrap;
        private readonly IAbilityToNoesisViewModelConverter _abilityToNoesisViewModelConverter;

        private Tuple<double, double> _translatedLeftResource;
        private Tuple<double, double> _translatedRightResource;
        private List<string> _abilitiesKeys;
        private ConcurrentDictionary<string, Tuple<double, string, ImageSource>> _translatedAbilities;

        static StatusBarNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<StatusBarNoesisViewModel>();
        }

        public StatusBarNoesisViewModel(
            IUiDispatcher uiDispatcher,
            IStatusBarViewModel viewModelToWrap,
            IAbilityToNoesisViewModelConverter abilityToNoesisViewModelConverter)
        {
            _translatedLeftResource = null;
            _translatedRightResource = null;
            _abilitiesKeys = new List<string>();
            _translatedAbilities = new ConcurrentDictionary<string, Tuple<double, string, ImageSource>>();

            _uiDispatcher = uiDispatcher;
            _viewModelToWrap = viewModelToWrap;
            _abilityToNoesisViewModelConverter = abilityToNoesisViewModelConverter;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;

            RefreshLeftResource();
            RefreshRightResource();
            RefreshAbilities();
        }

        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        // TODO: This has to happen here because the ImageSources need to be made on the UI thread
        // Find a better way to do this!
        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.Abilities))]
        public IReadOnlyCollection<Tuple<double, string, ImageSource>> Abilities =>
            _abilitiesKeys
                .Select(x => _translatedAbilities[x])
                .ToArray(); // NOTE: we must realize the enumerable before returning to satisfy Noesis

        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.LeftResource))]
        public Tuple<double, double> LeftResource => _translatedLeftResource;

        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.RightResource))]
        public Tuple<double, double> RightResource => _translatedRightResource;

        private void RefreshLeftResource()
        {
            _translatedLeftResource = Tuple.Create(
                _viewModelToWrap.LeftResource?.Current ?? 1,
                _viewModelToWrap.LeftResource?.Maximum ?? 1);
        }

        private void RefreshRightResource()
        {
            _translatedRightResource = Tuple.Create(
                _viewModelToWrap.RightResource?.Current ?? 1,
                _viewModelToWrap.RightResource?.Maximum ?? 1);
        }

        private void RefreshAbilities()
        {
            _uiDispatcher.RunOnMainThread(() =>
            {
                _abilitiesKeys.Clear();
                _translatedAbilities.Clear();

                foreach (var translated in _viewModelToWrap
                   .Abilities
                   .Select(_abilityToNoesisViewModelConverter.Convert))
                {
                    _abilitiesKeys.Add(translated.Item2);
                    _translatedAbilities[translated.Item2] = translated;
                }
            });
        }

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(_viewModelToWrap.LeftResource)))
            {
                RefreshLeftResource();
            }
            else if (e.PropertyName.Equals(nameof(_viewModelToWrap.RightResource)))
            {
                RefreshRightResource();
            }
            else if (e.PropertyName.Equals(nameof(_viewModelToWrap.Abilities)))
            {
                RefreshAbilities();
            }

            if (!_lazyNotifierMapping.Value.TryGetValue(
                e.PropertyName,
                out var propertyName))
            {
                return;
            }

            _uiDispatcher.RunOnMainThread(() =>
            {
                OnPropertyChanged(propertyName);
            });
        }
    }
}