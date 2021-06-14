#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Api.SceneTransitions;
using Macerus.Plugins.Features.Gui.Default;

namespace Assets.Scripts.Plugins.Features.SceneTransitions.Noesis
{
    public sealed class FaderSceneTransitionNoesisViewModel :
        NotifierBase,
        IFaderSceneTransitionNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IFaderSceneTransitionViewModel _viewModelToWrap;

        static FaderSceneTransitionNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<FaderSceneTransitionNoesisViewModel>();
        }

        public FaderSceneTransitionNoesisViewModel(IFaderSceneTransitionViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;
            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        public Visibility Visibility => Opacity > 0
            ? Visibility.Visible
            : Visibility.Collapsed;

        [NotifyForWrappedProperty(nameof(IFaderSceneTransitionViewModel.Opacity))]
        public double Opacity => _viewModelToWrap.Opacity;

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

            if (propertyName.Equals(nameof(Opacity)))
            {
                OnPropertyChanged(nameof(Visibility));
            }
        }
    }
}
