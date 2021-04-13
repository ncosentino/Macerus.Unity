#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IEquipGridItemSlotNoesisViewModel : IItemSlotNoesisViewModel
    {
        int GridRow { get; }

        int GridRowSpan { get; }

        int GridColumn { get; }

        int GridColumnSpan { get; }
    }
}
