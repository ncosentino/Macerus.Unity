using System;
using System.Collections;
using System.Threading.Tasks;

namespace Assets.Scripts.Unity.Threading
{
    public interface ICoroutineRunner
    {
        void StartCoroutine(
            IEnumerator coroutine,
            Action<Exception> errorCallback);

        Task<CoroutineResult<T>> RunCoroutineAsync<T>(
            IEnumerator coroutine,
            Func<T> getResultCallback);

        Task<CoroutineResult> RunCoroutineAsync(
            IEnumerator coroutine,
            Func<bool> checkCompletionCallback);
    }
}
