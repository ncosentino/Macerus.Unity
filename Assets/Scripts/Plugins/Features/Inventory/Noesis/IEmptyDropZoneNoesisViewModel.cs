#if UNITY_5_3_OR_NEWER
#define NOESIS

#else

#endif

using System.Windows.Input;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IEmptyDropZoneNoesisViewModel
    {
        bool IsDragOver { get; }

        ICommand DropItem { get; }
    }
}
