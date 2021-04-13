using Macerus.Plugins.Features.Inventory.Api;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IItemSlotToNoesisViewModelConverter
    {
        IItemSlotNoesisViewModel Convert(IItemSlotViewModel input);

        IItemSlotViewModel ConvertBack(IItemSlotNoesisViewModel input);
    }
}