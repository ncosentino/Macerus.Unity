#if UNITY_5_3_OR_NEWER
#define NOESIS

#else

#endif

using System.Windows.Input;
using Assets.Scripts.Gui.Noesis;

namespace Assets.Scripts.Plugins.Features.Inventory.Noesis
{
    public interface IEmptyDropZoneNoesisViewModel : ITransparentToGameInteraction
    {
        bool IsDragOver { get; }

        ICommand DropItem { get; }
    }
}
