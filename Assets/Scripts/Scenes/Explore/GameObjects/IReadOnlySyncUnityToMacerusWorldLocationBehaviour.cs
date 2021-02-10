using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlySyncUnityToMacerusWorldLocationBehaviour
    {
        IWorldLocationBehavior WorldLocationBehavior { get; }
    }
}