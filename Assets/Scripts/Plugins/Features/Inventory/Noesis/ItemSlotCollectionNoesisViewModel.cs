#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

using Assets.Scripts.Gui.Noesis;
using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Inventory.Api;
using Macerus.Plugins.Features.Gui.Default;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class ItemSlotCollectionNoesisViewModel :
        NotifierBase,
        IItemSlotCollectionNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IItemSlotToNoesisViewModelConverter _itemSlotToNoesisViewModelConverter;
        private readonly IItemSlotCollectionViewModel _viewModelToWrap;
        private readonly ConcurrentDictionary<object, IItemSlotNoesisViewModel> _itemSlotViewModels;

        static ItemSlotCollectionNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<ItemSlotCollectionNoesisViewModel>();
        }

        public ItemSlotCollectionNoesisViewModel(
            IItemSlotToNoesisViewModelConverter itemSlotToNoesisViewModelConverter,
            IItemSlotCollectionViewModel viewModelToWrap,
            ImageSource backgroundImageSource)
        {
            _itemSlotToNoesisViewModelConverter = itemSlotToNoesisViewModelConverter;
            _viewModelToWrap = viewModelToWrap;
            BackgroundImageSource = backgroundImageSource;

            _itemSlotViewModels = new ConcurrentDictionary<object, IItemSlotNoesisViewModel>();

            StartDragItem = new DelegateCommand(OnCanStartDragItem, OnStartDragItem);
            EndDragItem = new DelegateCommand(OnEndDragItem);
            DropItem = new DelegateCommand(OnCanDropItem, OnDropItem);

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        [NotifyForWrappedProperty(nameof(IItemSlotCollectionViewModel.ItemSlots))]
        public IEnumerable<IItemSlotNoesisViewModel> ItemSlots => _itemSlotViewModels.Values;

        public ImageSource BackgroundImageSource { get; }

        public ICommand StartDragItem { get; }

        public ICommand EndDragItem { get; }

        public ICommand DropItem { get; }

        [NotifyForWrappedProperty(nameof(IItemSlotCollectionViewModel.IsDragOver))]
        public bool IsDragOver
        {
            get { return _viewModelToWrap.IsDragOver; }
            set => _viewModelToWrap.IsDragOver = value;
        }

        private bool OnCanStartDragItem(object param)
        {
            var slot = (IItemSlotNoesisViewModel)param;
            return slot.HasItem; // FIXME: delegate this out because maybe we want to prevent dragging for other reasons
        }

        private void OnStartDragItem(object param)
        {
            var slot = (IItemSlotNoesisViewModel)param;
            var wrappedSlot = _itemSlotToNoesisViewModelConverter.ConvertBack(slot);
            _viewModelToWrap.StartDragItem(wrappedSlot);
        }

        private void OnEndDragItem(object param)
        {
            _viewModelToWrap.EndDragItem((bool)param);
        }

        private bool OnCanDropItem(object param)
        {
            var slot = param as IItemSlotNoesisViewModel; // allowed to be null! param can be this control
            var wrappedSlot = _itemSlotToNoesisViewModelConverter.ConvertBack(slot);
            return _viewModelToWrap.CanDropItem(wrappedSlot);
        }

        private void OnDropItem(object param)
        {
            var slot = param as IItemSlotNoesisViewModel; // allowed to be null! param can be this control
            var wrappedSlot = _itemSlotToNoesisViewModelConverter.ConvertBack(slot);
            _viewModelToWrap.DropItem(wrappedSlot);
            _viewModelToWrap.EndDragItem(true);
        }

        private void ViewModelToWrap_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(_viewModelToWrap.ItemSlots)))
            {
                _itemSlotViewModels.Clear();
                foreach (var slot in _viewModelToWrap
                   .ItemSlots
                   .Select(_itemSlotToNoesisViewModelConverter.Convert))
                {
                    _itemSlotViewModels[slot.Id] = slot;
                }
            }

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