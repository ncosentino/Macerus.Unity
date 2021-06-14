#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.HeaderBar.Api;

namespace Assets.Scripts.Scenes.LoadingScreen.Gui.Noesis
{
    public sealed class LoadingScreenNoesisViewModel :
        NotifierBase,
        ILoadingScreenNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IHeaderBarViewModel _viewModelToWrap;

        static LoadingScreenNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<LoadingScreenNoesisViewModel>();
        }

        public LoadingScreenNoesisViewModel(
            IHeaderBarViewModel viewModelToWrap)
        {
            _viewModelToWrap = viewModelToWrap;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        //[NotifyForWrappedProperty(nameof(IItemSlotCollectionViewModel.ItemSlots))]
        //public IReadOnlyCollection<IItemSlotNoesisViewModel> ItemSlots =>
        //    _itemSlotKeyOrdering
        //        .Select(x => _itemSlotViewModels[x])
        //        .ToArray(); // NOTE: we must realize the enumerable before returning to satisfy Noesis

        public ImageSource BackgroundImageSource { get; }

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