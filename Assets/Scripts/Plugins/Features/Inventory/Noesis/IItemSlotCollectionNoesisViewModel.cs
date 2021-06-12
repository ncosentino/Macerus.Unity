#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Collections.Generic;
using System.Windows.Input;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IItemSlotCollectionNoesisViewModel
    {
        IReadOnlyCollection<IItemSlotNoesisViewModel> ItemSlots { get; }

        ImageSource BackgroundImageSource { get; }

        ICommand StartDragItem { get; }

        ICommand EndDragItem { get; }

        ICommand DropItem { get; }

        bool IsDragOver { get; set; }
    }
}
