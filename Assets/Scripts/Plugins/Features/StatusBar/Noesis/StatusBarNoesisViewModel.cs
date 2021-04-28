﻿#if UNITY_5_3_OR_NEWER
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
            _translatedAbilities = new ConcurrentDictionary<string, Tuple<double, string, ImageSource>>();

            _uiDispatcher = uiDispatcher;
            _viewModelToWrap = viewModelToWrap;
            _abilityToNoesisViewModelConverter = abilityToNoesisViewModelConverter;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        // TODO: This has to happen here because the ImageSources need to be made on the UI thread
        // Find a better way to do this!
        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.Abilities))]
        public IEnumerable<Tuple<double, string, ImageSource>> Abilities => _translatedAbilities.Values;

        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.LeftResource))]
        public Tuple<double, double> LeftResource => _translatedLeftResource;

        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.RightResource))]
        public Tuple<double, double> RightResource => _translatedRightResource;

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(_viewModelToWrap.LeftResource)))
            {
                _translatedLeftResource = Tuple.Create(
                    _viewModelToWrap.LeftResource.Current,
                    _viewModelToWrap.LeftResource.Maximum);
            }

            if (e.PropertyName.Equals(nameof(_viewModelToWrap.RightResource)))
            {
                _translatedRightResource = Tuple.Create(
                    _viewModelToWrap.RightResource.Current,
                    _viewModelToWrap.RightResource.Maximum);
            }

            if (e.PropertyName.Equals(nameof(_viewModelToWrap.Abilities)))
            {
                _uiDispatcher.ExecuteAsync(() => 
                { 
                    _translatedAbilities.Clear();

                    foreach (var translated in _viewModelToWrap
                       .Abilities
                       .Select(_abilityToNoesisViewModelConverter.Convert))
                    {
                        _translatedAbilities[translated.Item2] = translated;
                    }

                    OnPropertyChanged(nameof(Abilities));
                });

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