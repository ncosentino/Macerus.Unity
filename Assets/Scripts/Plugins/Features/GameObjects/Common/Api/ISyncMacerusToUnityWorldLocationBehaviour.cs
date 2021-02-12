using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface ISyncMacerusToUnityWorldLocationBehaviour : IReadOnlySyncMacerusToUnityWorldLocationBehaviour
    {
        new IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; set; }
    }
}