using Macerus.Game.Api;

using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Application
{
    public sealed class SceneManager : ISceneManager
    {
        public void GoToScene(IIdentifier sceneId)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId.ToString());
        }
    }
}
