#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows.Media;
#endif

using System.Windows.Input;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IItemSlotNoesisViewModel
    {
        bool HasItem { get; }

        ImageSource IconImageSource { get; }

        Brush BackgroundBrush { get; }

        bool ShowLabel { get; }

        object Id { get; }

        string SlotLabel { get; }

        bool IsDragOver { get; set; }

        bool IsDropAllowed { get; set; }

        bool IsFocused { get; set; }

        ICommand PopulateHoverCardCommand { get; }
    }
}
