using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Unity.Threading
{
    public interface IDispatcher
    {
        bool IsMainThread { get; }

        void RunAsync(Action action);

        void RunAsync<T>(Action<T> action, T state);

        void RunAsync(Action<object> action, object state);

        void RunOnMainThread(Action action);

        Task RunOnMainThreadAsync(
            Action action,
            Func<bool> checkCompletedCallback);
    }
}
