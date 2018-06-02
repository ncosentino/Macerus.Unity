using Assets.Scripts.Unity.Threading;
using Macerus.Api.Behaviors;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlyUpdateWorldLocatiobBehaviour
    {
        IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; }

        IDispatcher Dispatcher { get; }
    }
}