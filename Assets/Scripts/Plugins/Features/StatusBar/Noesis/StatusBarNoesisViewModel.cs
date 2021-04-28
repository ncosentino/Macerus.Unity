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
        private List<Tuple<double, string, IIdentifier>> _translatedAbilities;

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
            _translatedAbilities = new List<Tuple<double, string, IIdentifier>>();

            _viewModelToWrap = viewModelToWrap;
            _resourceImageSourceFactory = resourceImageSourceFactory;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        // TODO: This has to happen here because the ImageSources need to be made on the UI thread
        // Find a better way to do this!
        [NotifyForWrappedProperty(nameof(IStatusBarViewModel.Abilities))]
        public IEnumerable<Tuple<double, string, ImageSource>> Abilities => _translatedAbilities
            .Select(x => Tuple.Create(x.Item1, x.Item2, _resourceImageSourceFactory.CreateForResourceId(x.Item3)));

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
                var n = new List<Tuple<double, string, IIdentifier>>();
                foreach (var ability in _viewModelToWrap.Abilities)
                {
                    n.Add(
                        Tuple.Create(
                            ability.IsEnabled ? 1.0d : 0.3d,
                            ability.AbilityName,
                            ability.IconResourceId));
                }

                _translatedAbilities = n;
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