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
using Macerus.Plugins.Features.StatusBar.Api;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public sealed class StatusBarNoesisViewModel :
        NotifierBase,
        IStatusBarNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IStatusBarViewModel _viewModelToWrap;
        private Tuple<double, double> _translatedLeftResource;
        private Tuple<double, double> _translatedRightResource;

        static StatusBarNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<StatusBarNoesisViewModel>();
        }

        public StatusBarNoesisViewModel(IStatusBarViewModel viewModelToWrap)
        {
            _translatedLeftResource = null;
            _translatedRightResource = null;

            _viewModelToWrap = viewModelToWrap;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

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