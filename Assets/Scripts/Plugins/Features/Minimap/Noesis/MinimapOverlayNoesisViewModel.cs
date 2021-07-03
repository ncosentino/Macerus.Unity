#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Media;
#endif

using System;
using System.ComponentModel;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Minimap;
using ProjectXyz.Shared.Framework;

namespace Assets.Scripts.Plugins.Features.Minimap.Noesis
{
    public sealed class MinimapOverlayNoesisViewModel :
        MacerusViewModelWrapper,
        IMinimapOverlayNoesisViewModel
    {
        private readonly IMinimapOverlayViewModel _viewModelToWrap;
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;
        private readonly Lazy<ImageSource> _lazyCameraSource;

        public MinimapOverlayNoesisViewModel(
            IMinimapOverlayViewModel viewModelToWrap,
            IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _viewModelToWrap = viewModelToWrap;
            _resourceImageSourceFactory = resourceImageSourceFactory;
            _lazyCameraSource = new Lazy<ImageSource>(() =>
            {
                var imageSource = _resourceImageSourceFactory.CreateForResourceId(new StringIdentifier("Minimap/MinimapRenderTexture"));
                return imageSource;
            });

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        [NotifyForWrappedProperty(nameof(IMinimapOverlayViewModel.IsOpen))]
        public Visibility Visibility => _viewModelToWrap.IsOpen
            ? Visibility.Visible
            : Visibility.Collapsed;

        public ImageSource CameraSource => _lazyCameraSource.Value;

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