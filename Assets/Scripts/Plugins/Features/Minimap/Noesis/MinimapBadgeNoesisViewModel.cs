#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System.ComponentModel;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Minimap;

namespace Assets.Scripts.Plugins.Features.Minimap.Noesis
{
    public sealed class MinimapBadgeNoesisViewModel :
        MacerusViewModelWrapper,
        IMinimapBadgeNoesisViewModel
    {
        private readonly IMinimapBadgeViewModel _viewModelToWrap;

        public MinimapBadgeNoesisViewModel(IMinimapBadgeViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        [NotifyForWrappedProperty(nameof(IMinimapBadgeViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
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