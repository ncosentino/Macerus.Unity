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

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api;

namespace Assets.Scripts.Plugins.Features.HeaderBar.Noesis
{
    public sealed class HeaderBarNoesisViewModel :
        NotifierBase,
        IHeaderBarNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IHeaderBarViewModel _viewModelToWrap;

        static HeaderBarNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<HeaderBarNoesisViewModel>();
        }

        public HeaderBarNoesisViewModel(
            IHeaderBarViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

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