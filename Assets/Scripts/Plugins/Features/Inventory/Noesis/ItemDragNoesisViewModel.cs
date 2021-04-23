using System;
using System.Collections.Generic;
using System.ComponentModel;

using Assets.Scripts.Gui.Noesis.ViewModels;

using Macerus.Plugins.Features.Gui.Default;
using Macerus.Plugins.Features.Inventory.Api;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class ItemDragNoesisViewModel :
        NotifierBase,
        IItemDragNoesisViewModel
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> _lazyNotifierMapping;

        private readonly IItemSlotToNoesisViewModelConverter _itemSlotToNoesisViewModelConverter;
        private readonly IItemDragViewModel _viewModelToWrap;

        static ItemDragNoesisViewModel()
        {
            _lazyNotifierMapping = LazyNotifierMappingBuilder.BuildMapping<ItemDragNoesisViewModel>();
        }

        public ItemDragNoesisViewModel(
            IItemSlotToNoesisViewModelConverter itemSlotToNoesisViewModelConverter,
            IItemDragViewModel viewModelToWrap)
        {
            _itemSlotToNoesisViewModelConverter = itemSlotToNoesisViewModelConverter;
            _viewModelToWrap = viewModelToWrap;

            _viewModelToWrap.PropertyChanged += ViewModelToWrap_PropertyChanged;
        }

        [NotifyForWrappedProperty(nameof(IItemDragViewModel.DraggedItemSlot))]
        public IItemSlotNoesisViewModel DraggedItemSlot
        {
            get => _itemSlotToNoesisViewModelConverter.Convert(_viewModelToWrap.DraggedItemSlot);
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