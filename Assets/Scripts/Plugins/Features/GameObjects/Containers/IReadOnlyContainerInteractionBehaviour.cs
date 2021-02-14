using Assets.Scripts.Unity.GameObjects;

using ProjectXyz.Api.GameObjects;

namespace Assets.Scripts.Plugins.Features.GameObjects.Containers
{
    public interface IReadOnlyContainerInteractionBehaviour
    {
        IGameObjectManager GameObjectManager { get; }

        IObjectDestroyer ObjectDestroyer { get; }
    }
}