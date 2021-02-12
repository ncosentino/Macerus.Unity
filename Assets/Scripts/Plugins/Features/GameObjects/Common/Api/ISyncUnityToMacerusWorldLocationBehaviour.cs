using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface ISyncUnityToMacerusWorldLocationBehaviour : IReadOnlySyncUnityToMacerusWorldLocationBehaviour
    {
        new IWorldLocationBehavior WorldLocationBehavior { get; set; }
    }
}