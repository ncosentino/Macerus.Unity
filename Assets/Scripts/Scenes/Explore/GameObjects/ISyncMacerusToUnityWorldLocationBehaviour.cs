using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface ISyncMacerusToUnityWorldLocationBehaviour : IReadOnlySyncMacerusToUnityWorldLocationBehaviour
    {
        new IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; set; }
    }
}