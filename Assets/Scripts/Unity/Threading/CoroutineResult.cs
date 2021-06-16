using System;

namespace Assets.Scripts.Unity.Threading
{
    public sealed class CoroutineResult
    {
        public CoroutineResult(Exception exception)
        {
            Exception = exception;
        }

        public bool Success => Exception == null;

        public Exception Exception { get; }
    }

    public sealed class CoroutineResult<T>
    {
        public CoroutineResult(Exception exception)
        {
            Exception = exception;
        }

        public CoroutineResult(T result)
        {
            Result = result;
        }

        public bool Success => Exception == null;

        public Exception Exception { get; }

        public T Result { get; }
    }
}
