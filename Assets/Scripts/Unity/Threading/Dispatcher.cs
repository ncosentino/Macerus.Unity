using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Unity.Threading
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly Lazy<IDispatcher> _instance;

        private IDispatcher Instance => _instance.Value;

        public Dispatcher(Func<IDispatcher> getInstanceCallback)
        {
            _instance = new Lazy<IDispatcher>(getInstanceCallback);
        }

        public bool IsMainThread => Instance.IsMainThread;

        public void RunAsync(Action action) => Instance.RunAsync(action);

        public void RunAsync<T>(Action<T> action, T state) => Instance.RunAsync(action, state);

        public void RunAsync(Action<object> action, object state) => Instance.RunAsync(action, state);

        public void RunOnMainThread(Action action) => Instance.RunOnMainThread(action);

        public async Task RunOnMainThreadAsync(
            Action action,
            Func<bool> checkCompletedCallback) => await Instance.RunOnMainThreadAsync(
                action,
                checkCompletedCallback);
    }
}
