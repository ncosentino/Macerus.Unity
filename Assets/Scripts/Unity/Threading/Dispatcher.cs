using System;

namespace Assets.Scripts.Unity.Threading
{
    public sealed class Dispatcher : IDispatcher
    {
        private readonly Lazy<IDispatcher> _instance;
        private readonly Func<IDispatcher> _getInstanceCallback;

        private IDispatcher Instance => _instance.Value;

        public Dispatcher(Func<IDispatcher> getInstanceCallback)
        {
            _instance = new Lazy<IDispatcher>(getInstanceCallback);
        }

        public void RunAsync(Action action) => Instance.RunAsync(action);

        public void RunAsync<T>(Action<T> action, T state) => Instance.RunAsync(action, state);

        public void RunAsync(Action<object> action, object state) => Instance.RunAsync(action, state);

        public void RunOnMainThread(Action action) => Instance.RunOnMainThread(action);
    }
}
