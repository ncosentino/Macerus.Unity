using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers
{
    public interface IContainerInteractionBehaviour : IReadOnlyContainerInteractionBehaviour
    {
        new IGameObjectManager GameObjectManager { get; set; }

        new IObjectDestroyer ObjectDestroyer { get; set; }
    }
}