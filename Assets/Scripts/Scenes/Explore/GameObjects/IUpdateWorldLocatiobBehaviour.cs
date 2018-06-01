using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IUpdateWorldLocatiobBehaviour : IReadOnlyUpdateWorldLocatiobBehaviour
    {
        new IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; set; }
    }
}