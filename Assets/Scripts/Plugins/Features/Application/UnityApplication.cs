using Macerus.Game.Api;

namespace Assets.Scripts.Plugins.Features.Application
{
    public sealed class UnityApplication : IApplication
    {
        private readonly IObservableUpdateFrequencyManager _updateFrequencyManager;

        public UnityApplication(IObservableUpdateFrequencyManager updateFrequencyManager)
        {
            _updateFrequencyManager = updateFrequencyManager;
            _updateFrequencyManager.MaxUpdatesPerSecondChanged += (_, __) => EnforceFrameRate();
            EnforceFrameRate();
        }

        public void Exit()
        {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            global::UnityEditor.EditorApplication.isPlaying = false;
#else
         UnityEngine.Application.Quit();
#endif
        }

        private void EnforceFrameRate()
        {
            UnityEngine.Application.targetFrameRate = _updateFrequencyManager.MaxUpdatesPerSecond;
        }
    }
}
