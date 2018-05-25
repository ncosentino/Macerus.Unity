using System;

namespace Assets.Scripts.Unity.Threading
{
    public interface IDispatcher
    {
        void RunAsync(Action action);

        void RunAsync<T>(Action<T> action, T state);

        void RunAsync(Action<object> action, object state);

        void RunOnMainThread(Action action);
    }
}
