using System;
using ProjectXyz.Api.Logging;

namespace Assets.Scripts.Logging
{
    public sealed class ConsoleLogger : ILogger
    {
        public void Debug(string message) => Debug(message, null);

        public void Debug(string message, object data)
        {
            UnityEngine.Debug.LogFormat($"DEBUG: {message}", data);
        }

        public void Info(string message) => Info(message, null);

        public void Info(string message, object data)
        {
            UnityEngine.Debug.LogFormat(message, data);
        }

        public void Warn(string message) => Warn(message, null);

        public void Warn(string message, object data)
        {
            UnityEngine.Debug.LogWarningFormat(message, data);
        }

        public void Error(string message) => Error(message, null);

        public void Error(string message, object data)
        {
            UnityEngine.Debug.LogErrorFormat(message, data);
        }
    }
}
