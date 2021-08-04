#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System.Windows.Input;

namespace Assets.Scripts.Plugins.Features.Gui
{
    public interface IModalButtonNoesisViewModel
    {
        string Text { get; }

        ICommand Command { get; }
    }
}
