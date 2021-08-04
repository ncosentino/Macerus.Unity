#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System.Windows.Input;

namespace Assets.Scripts.Plugins.Features.Gui
{
    public sealed class ModalButtonNoesisViewModel : IModalButtonNoesisViewModel
    {
        public ModalButtonNoesisViewModel(
            string text,
            ICommand command)
        {
            Text = text;
            Command = command;
        }

        public string Text { get; }

        public ICommand Command { get; }
    }
}
