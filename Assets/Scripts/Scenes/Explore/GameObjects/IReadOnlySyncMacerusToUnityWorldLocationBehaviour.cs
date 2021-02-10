using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlySyncMacerusToUnityWorldLocationBehaviour
    {
        IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; }
    }
}