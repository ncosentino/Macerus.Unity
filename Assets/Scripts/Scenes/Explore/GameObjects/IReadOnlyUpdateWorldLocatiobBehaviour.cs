using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlyUpdateWorldLocatiobBehaviour
    {
        IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; }
    }
}