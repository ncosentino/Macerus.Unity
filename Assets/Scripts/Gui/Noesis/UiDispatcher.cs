#if UNITY_5_3_OR_NEWER
#define NOESIS
using Assets.Scripts.Unity.Threading;
#else
#endif

using System;

namespace Assets.Scripts.Gui.Noesis
{
    public sealed class UiDispatcher : IUiDispatcher
    {
#if NOESIS
        private readonly IDispatcher _dispatcher;

        public UiDispatcher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void RunOnMainThread(Action action)
        {
            _dispatcher.RunOnMainThread(action);
        }
#else
        public void RunOnMainThread(Action action) => action();
#endif
    }
}
