#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System.Windows;
#endif

using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class UiDispatcher : IUiDispatcher
    {
        public Task ExecuteAsync(Action action)
        {
#if NOESIS
            var dispatcher = Dispatcher.CurrentDispatcher;
            if (dispatcher == null)
            {
                return Task.CompletedTask;
            }

            return Task.Run(() =>
            {
                dispatcher.BeginInvoke(action);
            });
#else
            var dispatcher = Application.Current.Dispatcher;

            return dispatcher == null
                ? Task.CompletedTask
                : dispatcher.InvokeAsync(action).Task;
#endif
        }
    }
}
