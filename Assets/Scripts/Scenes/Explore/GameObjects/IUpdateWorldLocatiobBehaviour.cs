using Assets.Scripts.Unity.Threading;
using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IUpdateWorldLocatiobBehaviour : IReadOnlyUpdateWorldLocatiobBehaviour
    {
        new IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; set; }

        new IDispatcher Dispatcher { get; set; }
    }
}