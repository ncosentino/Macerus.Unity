using System;
using System.Collections;

namespace Assets.Scripts.Unity.Threading
{
    public sealed class CoroutineRunner : ICoroutineRunner
    {
        private readonly Lazy<ICoroutineRunner> _wrapped;
        private readonly Lazy<IDispatcher> _dispatcher;

        public CoroutineRunner(
            Lazy<ICoroutineRunner> wrapped,
            Lazy<IDispatcher> dispatcher)
        {
            _wrapped = wrapped;
            _dispatcher = dispatcher;
        }

        public void StartCoroutine(
            IEnumerator coroutine,
            Action<Exception> errorCallback)
        {
            _dispatcher.Value.RunOnMainThread(() =>
            {
                _wrapped.Value.StartCoroutine(
                    coroutine,
                    errorCallback);
            });
        }
    }
}
