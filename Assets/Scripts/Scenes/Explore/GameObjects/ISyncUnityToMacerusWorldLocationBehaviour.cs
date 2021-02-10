using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface ISyncUnityToMacerusWorldLocationBehaviour : IReadOnlySyncUnityToMacerusWorldLocationBehaviour
    {
        new IWorldLocationBehavior WorldLocationBehavior { get; set; }
    }
}