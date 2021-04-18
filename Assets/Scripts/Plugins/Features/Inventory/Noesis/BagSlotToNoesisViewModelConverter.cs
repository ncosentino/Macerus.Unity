#if UNITY_5_3_OR_NEWER
#define NOESIS

#else

#endif

using System;
using Macerus.Plugins.Features.Inventory.Api;
using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public sealed class BagSlotToNoesisViewModelConverter : IBagSlotToNoesisViewModelConverter
    {
        private readonly IResourceImageSourceFactory _resourceImageSourceFactory;

        public BagSlotToNoesisViewModelConverter(IResourceImageSourceFactory resourceImageSourceFactory)
        {
            _resourceImageSourceFactory = resourceImageSourceFactory;
        }

        public IItemSlotNoesisViewModel Convert(IItemSlotViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            var imageSource = input.IconResourceId == null
                ? null
                : _resourceImageSourceFactory.CreateForResourceId(input.IconResourceId);
            var viewModel = new ItemSlotNoesisViewModel(
                input,
                imageSource);
            return viewModel;
        }

        public IItemSlotViewModel ConvertBack(IItemSlotNoesisViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            // FIXME: i am sorry for this filth.
            if (!(input is ItemSlotNoesisViewModel))
            {
                throw new NotSupportedException(
                    $"Currently there is only support for converting back from '{typeof(ItemSlotNoesisViewModel)}'.");
            }

            return ((ItemSlotNoesisViewModel)input).ViewModelToWrap;
        }
    }
}