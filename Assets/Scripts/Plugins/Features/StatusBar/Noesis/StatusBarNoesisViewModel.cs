#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Media;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.StatusBar.Api;
using Assets.Scripts.Gui.Noesis;
using System.Collections.ObjectModel;
using System.Threading;
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.StatusBar.Noesis
{
    public sealed class StatusBarNoesisViewModel :
        NotifierBase,
        IStatusBarNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IStatusBarViewModel _viewModelToWrap;
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;

        private Tuple<double, double> _translatedLeftResource;
        private Tuple<double, double> _translatedRightResource;
        private IIdentifier _source;
        private IIdentifier _sourceTwo;
        private string _name;
        private string _nameTwo;

        static StatusBarNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<StatusBarNoesisViewModel>();
        }

        public StatusBarNoesisViewModel(
            IStatusBarViewModel viewModelToWrap,
            IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _translatedLeftResource = null;
            _translatedRightResource = null;
            _source = null;
            _sourceTwo = null;
            _name = "";
            _nameTwo = "";

            _viewModelToWrap = viewModelToWrap;
            _resourceImageSourceFactory = resourceImageSourceFactory;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        public ImageSource Source => _resourceImageSourceFactory.CreateForResourceId(_source);

        public string Name => _name;

        public ImageSource SourceTwo => _resourceImageSourceFactory.CreateForResourceId(_sourceTwo);

        public string NameTwo => _nameTwo;

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
                var x = _viewModelToWrap.Abilities.FirstOrDefault();
                if (x == null)
                {
                    return;
                }

                _name = x.AbilityName;
                _source = x.IconResourceId;

                var y = _viewModelToWrap.Abilities.LastOrDefault();
                if (y == null)
                {
                    return;
                }

                _nameTwo = y.AbilityName;
                _sourceTwo = y.IconResourceId;

                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Source));
                OnPropertyChanged(nameof(NameTwo));
                OnPropertyChanged(nameof(SourceTwo));
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