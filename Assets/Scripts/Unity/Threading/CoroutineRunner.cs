using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<CoroutineResult<T>> RunCoroutineAsync<T>(
            IEnumerator coroutine,
            Func<T> getResultCallback)
        {
            Exception coroutineError = null;
            StartCoroutine(
                coroutine,
                ex => coroutineError = ex);

            T resultFromCallback = default;
            await WaitForConditionAsync(() =>
            {
                if (coroutineError != null)
                {
                    return true;
                }

                resultFromCallback = getResultCallback();
                return !EqualityComparer<T>.Default.Equals(resultFromCallback, default);
            })
            .ConfigureAwait(false);

            var result = coroutineError != null
                ? new CoroutineResult<T>(coroutineError)
                : new CoroutineResult<T>(null);
            return result;
        }

        public async Task<CoroutineResult> RunCoroutineAsync(
            IEnumerator coroutine,
            Func<bool> checkCompletionCallback)
        {
            Exception coroutineError = null;
            StartCoroutine(
                coroutine,
                ex => coroutineError = ex);

            await WaitForConditionAsync(() => coroutineError != null || checkCompletionCallback())
                .ConfigureAwait(false);

            var result = new CoroutineResult(coroutineError);
            return result;
        }

        private async Task WaitForConditionAsync(Func<bool> callback)
        {
            while (!callback())
            {
                await Task.Delay(0);
            }
        }
    }
}
