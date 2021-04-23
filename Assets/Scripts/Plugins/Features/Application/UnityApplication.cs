using Macerus.Game.Api;

namespace Assets.Scripts.Plugins.Features.Application
{
    public sealed class UnityApplication : IApplication
    {
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
    }
}
