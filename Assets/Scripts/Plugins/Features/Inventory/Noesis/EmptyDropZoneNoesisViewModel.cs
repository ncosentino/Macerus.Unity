#if UNITY_5_3_OR_NEWER
#define NOESIS

#else

#endif

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class EmptyDropZoneNoesisViewModel :
        NotifierBase,
        IEmptyDropZoneNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IItemSlotCollectionViewModel _viewModelToWrap;

        static EmptyDropZoneNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<ItemSlotCollectionNoesisViewModel>();
        }

        public EmptyDropZoneNoesisViewModel(IItemSlotCollectionViewModel viewModelToWrap)
        {
            DropItem = new DelegateCommand(OnCanDropItem, OnDropItem);
            _viewModelToWrap = viewModelToWrap;
        }

        public ICommand DropItem { get; }

        [NotifyForWrappedProperty(nameof(IItemSlotCollectionViewModel.IsDragOver))]
        public bool IsDragOver
        {
            get { return _viewModelToWrap.IsDragOver; }
            set => _viewModelToWrap.IsDragOver = value;
        }

        private bool OnCanDropItem(object param) => true;

        private void OnDropItem(object param)
        {
            _viewModelToWrap.DropItem(null);
            _viewModelToWrap.EndDragItem(true);
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

            OnPropertyChanged(e.PropertyName);
        }
    }
}
