#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System;

using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    using IItemSlotViewModel = Macerus.Plugins.Features.Inventory.Api.IItemSlotViewModel;

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

            var backgroundBrush = input.SlotBackgroundColor == null
                ? new SolidColorBrush(Color.FromArgb(0x40, 0xDA, 0x98, 0x58))
                : new SolidColorBrush(Color.FromArgb(0x40, (byte)input.SlotBackgroundColor.R, (byte)input.SlotBackgroundColor.G, (byte)input.SlotBackgroundColor.B));
            var iconColor = input.IconColor == null
                ? Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF)
                : Color.FromArgb((byte)input.IconColor.A, (byte)input.IconColor.R, (byte)input.IconColor.G, (byte)input.IconColor.B);
            var iconOpacity = input.IconColor == null
                ? 1
                : input.IconOpacity;

            var imageSource = input.IconResourceId == null
                ? null
                : _resourceImageSourceFactory.CreateForResourceId(input.IconResourceId);
            var viewModel = new ItemSlotNoesisViewModel(
                input,
                imageSource,
                backgroundBrush,
                iconOpacity,
                iconColor);
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